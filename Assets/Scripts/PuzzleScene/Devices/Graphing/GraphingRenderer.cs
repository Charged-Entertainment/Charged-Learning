using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace CLGraphing
{
    public class GraphingRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        public Graph graph { get; private set; }

        /// <summary>
        /// The points on the rendered segment of the graph.
        /// </summary>
        private NativeArray<Vector3> Points { get { return graph.GetSegment(FirstIdx, LastIdx); } }
        public int FirstIdx { get; private set; } = 0;
        public int LastIdx { get; private set; } = 0;

        private void Awake()
        {
            graph = GetComponent<Graph>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
        }

        private void Update() {
            var seg = Points;
            lineRenderer.positionCount = seg.Length;
            lineRenderer.SetPositions(seg);

            transform.position = new Vector3(-First.x * Scale.x, transform.position.y, transform.position.z);
            transform.position += transform.parent.position;

            FitVertically();
            if (UnderflowX > 0) FitHorizontally();
            
            Debug.Log($"Value: {Graphing.cursor.Value}, FirstIdx: {FirstIdx}, CurrentIdx: {Graphing.cursor.CurrentIdx}, LastIdx: {LastIdx}");
        }

        private float OverflowX { get { return WorldLength - Graphing.MAX_WIDTH; } }
        private float UnderflowX { get { return Graphing.MAX_WIDTH - WorldLength; } }
        public void FitHorizontally(bool right = true)
        {
            LastIdx = Mathf.Clamp(LastIdx, 0, graph.LastIdx);
            FirstIdx = Mathf.Clamp(FirstIdx, 0, graph.LastIdx);

            Action handleOverflowX = right ? () => { FirstIdx++; } : () => { LastIdx--; };
            while (OverflowX > 0) handleOverflowX();
            while (LastIdx != graph.LastIdx && UnderflowX > 0) LastIdx++;
        }

        ///<summary>
        /// Fit (scale) the graphs vertically to fit the maximum y value (max rendered value of all graphs). Pass true to fit to the maximum global value instead (even if it's not currently rendered).
        ///</summary>
        public void FitVertically(bool global = false)
        {
            float max = 0;
            Func<Graph, float> getMax = global ? (g => g.AbsMax) : (g => g.renderer.AbsMax);
            // foreach (var graph in graphs.Values) 
            max = Mathf.Max(max, getMax(graph));
            if (max == 0) return;
            Graphing.container.transform.localScale = new Vector3(Scale.x, Graphing.MAX_HEIGHT / max, Scale.z);
        }

        ///<summary>
        /// Move the displayed portion of the graph 'distance' indices to the right. Automatically calls Draw().
        ///</summary>
        public void ScrollRight(int distance = 1) { LastIdx += Mathf.Abs(distance); FitHorizontally(); }

        ///<summary>
        /// Move the displayed portion of the graph 'distance' indices to the right. Automatically calls Draw().
        ///</summary>
        public void ScrollLeft(int distance = 1) { FirstIdx -= Mathf.Abs(distance); FitHorizontally(false); }

        public void SnapRight() { ScrollRight(graph.LastIdx - LastIdx); }
        public void SnapLeft() { ScrollLeft(FirstIdx); }

        /// <summary>
        /// The world position of the i-th point on the graph.
        /// </summary>
        public Vector3 At(int i)
        {
            var p = graph.At(i);
            return new Vector3(p.x * Scale.x, p.y * Scale.y, p.z * Scale.z) + transform.position;
        }
        public float AbsMax { get { return Points.Max(p => Mathf.Abs(p.y)); } }
        public Vector2 First { get { return Points.First(); } }
        public Vector2 Last { get { return Points.Last(); } }
        public float RawLength { get { return Last.x - First.x; } }
        public float WorldLength { get { return RawLength * Scale.x; } }
        public Vector3 Scale { get { return Graphing.CurrentScale; } }
        public Vector3 InverseScale { get { return new Vector3(1 / Graphing.CurrentScale.x, 1 / Graphing.CurrentScale.y, 1 / Graphing.CurrentScale.z); } }
        public float Width { get { return lineRenderer.startWidth; } }
        public Color Color { get { return lineRenderer.startColor; } }
    }
}