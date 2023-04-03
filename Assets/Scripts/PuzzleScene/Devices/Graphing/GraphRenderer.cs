using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public partial class Graph : MonoBehaviour
{
    private class GraphRenderer : MonoBehaviour
    {
        // TEMP!! use Graphing.MAX_WIDTH instead. Using this to test with inspector for now.
        [SerializeField] float MAX_WIDTH = 5f;
        private LineRenderer lineRenderer;
        private Graph graph;


        public NativeArray<Vector3> RenderedSegment { get { return graph.Points.AsArray().GetSubArray(FirstIdxRendered, (LastIdxRendered - FirstIdxRendered) + 1); } }
        public int FirstIdxRendered { get; private set; } = 0;
        public int LastIdxRendered { get; private set; } = 0;

        private void Awake()
        {
            graph = GetComponent<Graph>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
        }

        enum ScrollDirection { Right = 1, Left = -1 }
        private void Scroll(int distance, ScrollDirection dir)
        {
            if (RenderedSegment.Length != 0)
            {
                LastIdxRendered += distance * (int)dir;
                LastIdxRendered = Mathf.Clamp(LastIdxRendered, 0, graph.LastIndex);

                float overflow = Width - (MAX_WIDTH * (1/Scale.x));
                Action adjust = (dir == ScrollDirection.Right) ? () => { FirstIdxRendered++; } : () => { LastIdxRendered--; };
                while (overflow > 0) { adjust(); overflow = Width - (MAX_WIDTH * (1/Scale.x)); }
            }
            Draw();
        }

        ///<summary>
        /// Move the displayed portion of the graph 'distance' indices to the right. Automatically calls Draw().
        ///</summary>
        public void ScrollRight(int distance = 1) { Scroll(distance, ScrollDirection.Right); }

        ///<summary>
        /// Move the displayed portion of the graph 'distance' indices to the right. Automatically calls Draw().
        ///</summary>
        public void ScrollLeft(int distance = 1) { Scroll(distance, ScrollDirection.Left); }

        ///<summary>
        /// Fit (scale) the graph vertically to fit the maximum y value (max rendered value). Pass true to fit to the maximum global value instead (even if it's not currently rendered).
        ///</summary>
        public void FitVertically(bool global = false)
        {
            var max = global ? graph.AbsMax : AbsMax;
            if (max == 0) return;
            transform.localScale = new Vector3(Scale.x, Graphing.MAX_HEIGHT / max, Scale.z);
        }

        public void Zoom(float factor)
        {
            transform.localScale = new Vector3(Scale.x * factor, Scale.y, Scale.z);
        }

        ///<summary> 
        /// Render the graph segment in the interval [FirstIdxRendered, LastIdxRendered].
        ///</summary> 
        public void Draw()
        {
            var seg = RenderedSegment;
            lineRenderer.positionCount = seg.Length;
            lineRenderer.SetPositions(seg);

            transform.position = new Vector3(-First.x * Scale.x,transform.position.y,transform.position.z);

            FitVertically(); // automatically fit
        }

        public Vector3 Scale { get { return transform.localScale; } }
        public float AbsMax { get { return RenderedSegment.Max(p => Mathf.Abs(p.y)); } }
        public Vector2 First { get { return RenderedSegment.First(); } }
        public Vector2 Last { get { return RenderedSegment.Last(); } }
        public float Width { get { return Last.x - First.x; } }
    }
}