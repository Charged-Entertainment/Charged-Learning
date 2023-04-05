using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace CLGraphing
{
    public class Graph : MonoBehaviour
    {
        private static readonly int startCapacity = 100;
        // size of the data ~= 12 bytes * Points.Capacity (starts with Capacity=startCapacity) and NativeLists support automatic reallocation
        [SerializeField] private NativeList<Vector3> Points;
        public new GraphingRenderer renderer { get; private set; }
        private void Awake()
        {
            Points = new NativeList<Vector3>(startCapacity, Allocator.Persistent);
            renderer = gameObject.AddComponent<GraphingRenderer>();
        }

        private void OnDisable() { Points.Dispose(); }


        public float Min { get; private set; } = float.MaxValue;
        public float Max { get; private set; } = float.MinValue;
        public float AbsMax { get { return Mathf.Max(Mathf.Abs(Min), Mathf.Abs(Max)); } }
        public void AddPoint(Vector2 point)
        {
            if (point.y < Min) Min = point.y;
            if (point.y > Max) Max = point.y;
            Points.Add(point);
        }

        /// <summary> 
        /// Get a segment from the graph from startIdx to endIdx (inclusive). 
        /// </summary> 
        public NativeArray<Vector3> GetSegment(int startIdx, int endIdx) { return Points.AsArray().GetSubArray(startIdx, endIdx - startIdx + 1); }

        /// <summary>
        /// The value of the point at index i of the graph.
        /// </summary>
        public Vector3 At(int i) { return Points[i]; }

        public Vector2 Scale { get { return transform.localScale; } }

        public int LastIdx { get { return Points.Length - 1; } }
        public Vector3 Last { get { return Points[Points.Length - 1]; } }
        public int Length { get { return Points.Length; } }
    }
}
