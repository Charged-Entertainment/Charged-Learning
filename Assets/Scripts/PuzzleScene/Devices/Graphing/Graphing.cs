using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CLGraphing
{
    [DisallowMultipleComponent]
    public class Graphing : MonoBehaviour
    {
        static public float time = 0f;
        static public Graphing instance { get; private set; }
        static public Vector3 CurrentScale { get { return container.transform.localScale; } }
        static public Graph activeGraph;
        static public GameObject container;
        static public GraphingController controller;
        static public GraphingCursor cursor;
        static public float maxHeight { get; private set; } = 2f;
        static public float maxWidth { get; private set; } = 4f;
        private void Awake()
        {
            if (instance == null) instance = this;
            else { GameObject.Destroy(this); return; }

            maxWidth = GraphingTool.display.width * 0.8f;
            maxHeight = (GraphingTool.display.height / 2) * 0.8f;
            container = GraphingTool.display.container;
            container.transform.position -= Vector3.right * maxWidth / 2;
            controller = gameObject.AddComponent<GraphingController>();
        }

        private void Update()
        {
            time += Time.deltaTime;
        }

        static public void CreateGraph(string label = "", string unit = "")
        {
            time = 0f;
            activeGraph = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/Graph").GetComponent<Graph>(), container.transform);
            if (cursor == null) cursor = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/GraphCursor").GetComponent<GraphingCursor>(), instance.transform);
            cursor.AttachTo(activeGraph);
        }
        static public void Zoom(float factor, bool isOffset = true)
        {
            if (!isOffset) container.transform.localScale = new Vector3(factor, CurrentScale.y, CurrentScale.z);
            container.transform.localScale = new Vector3(CurrentScale.x * factor, CurrentScale.y, CurrentScale.z);
            activeGraph.renderer.FitHorizontally(factor < 0);
        }

        static public void AddPoint(Vector2 point)
        {
            activeGraph.AddPoint(point);
        }

        ///<summary>
        /// Delete all graphs.
        ///</summary>
        public void Clear() { GameObject.Destroy(activeGraph.gameObject); activeGraph = null; }
    }
}
