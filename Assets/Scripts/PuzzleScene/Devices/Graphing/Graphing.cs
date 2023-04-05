using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CLGraphing
{
    public class Graphing : Singleton<Graphing>
    {
        static public Vector3 CurrentScale { get { return container.transform.localScale; } }
        static public Vector3 Position { get { return container.transform.position; } }
        static public Graph graph;
        static public GameObject container;
        static public GraphingController controller;
        static public GraphingCursor cursor;
        static public float MAX_HEIGHT { get; private set; } = 2f;
        static public float MAX_WIDTH { get; private set; } = 4f;
        [SerializeField] float maxWidth = MAX_WIDTH;
        [SerializeField] float maxHeight = MAX_HEIGHT;
        private new void Awake()
        {
            base.Awake();
            container = new GameObject("Graphs");
            container.transform.parent = transform;
            container.transform.position += transform.position;

            graph = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/Graph").GetComponent<Graph>(), container.transform);
            cursor = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/GraphCursor").GetComponent<GraphingCursor>(), transform);
            controller = gameObject.AddComponent<GraphingController>();
        }
        
        public static void Zoom(float factor, bool isOffset = true)
        {
            if (!isOffset) container.transform.localScale = new Vector3(factor, CurrentScale.y, CurrentScale.z);
            container.transform.localScale = new Vector3(CurrentScale.x * factor, CurrentScale.y, CurrentScale.z);
            graph.renderer.FitHorizontally(factor < 0);
        }

        ///<summary>
        /// Delete all graphs. (Does not clear graphs).
        ///</summary>
        public static void Clear() { GameObject.Destroy(graph.gameObject); graph = null; }

        
        
        #region test
        public int mult = 1;
        private void Update()
        {
            MAX_HEIGHT = maxHeight;
            MAX_WIDTH = maxWidth;

            var y = mult * Mathf.Sin(Time.time);
            graph.AddPoint(new Vector2(Time.time, y));
        }
        #endregion

    }
}
