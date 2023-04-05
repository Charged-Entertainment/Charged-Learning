using UnityEngine;


namespace CLGraphing
{
    /// <summary>
    /// Attach to graphs to move along them and read values from them.
    /// </summary>
    public class GraphingCursor : MonoBehaviour
    {
        public bool SnapedToEnd { get; private set; } = true;
        public int CurrentIdx { get; private set; } = 0;
        public bool Attached { get { return renderer != null; } }
        public Vector2 Value { get { return graph.At(CurrentIdx); } }

        new GraphingRenderer renderer;
        Graph graph;
        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            // try to attach to any graph
            AttachTo(GameObject.FindObjectOfType<Graph>());
        }

        private void Update()
        {
            if (renderer != null)
            {
                CurrentIdx = Mathf.Clamp(CurrentIdx, renderer.FirstIdx, renderer.LastIdx);
                transform.position = renderer.At(CurrentIdx);
                if (SnapedToEnd)
                {
                    renderer.SnapRight();
                    CurrentIdx = renderer.LastIdx;
                }
                SnapedToEnd = Utils.Approximately(CurrentIdx, graph.LastIdx, 1);
            }
        }

        /// <summary>
        /// Move i indices along the graph, or move directly to i if isOffset is false;
        /// </summary>
        public void Move(int i, bool isOffset = true)
        {
            SnapedToEnd = false;
            if (CurrentIdx == renderer.LastIdx && i > 0) renderer.ScrollRight(i);
            else if (CurrentIdx == renderer.FirstIdx && i < 0) renderer.ScrollLeft(i);

            if (isOffset) CurrentIdx += i;
            else CurrentIdx = i;
        }

        /// <summary>
        /// Attach to the given graph. Will de-attach from old graph if there was one.
        /// </summary>
        public void AttachTo(Graph graph)
        {
            this.graph = graph;
            this.renderer = graph.renderer;
            spriteRenderer.color = renderer.Color;
            transform.localScale = Vector3.one * renderer.Width * 5;
        }

        /// <summary>
        ///[NOT IMPLEMENTED YET] Attach to the next graph available in Graphing.
        /// </summary>
        public void JumpToNextGraph(Graph graph)
        {
            // Attach(Graphing.Graphs[(indexof(graph) + 1) % Graphing.Graphs.Count]);
        }

        /// <summary>
        ///[NOT IMPLEMENTED YET] Attach to the previous graph available in Graphing. 
        /// </summary>
        public void JumpToPreviousGraph(Graph graph)
        {
            // Attach(Graphing.Graphs[(indexof(graph) - 1) % Graphing.Graphs.Count]);
        }
    }
}