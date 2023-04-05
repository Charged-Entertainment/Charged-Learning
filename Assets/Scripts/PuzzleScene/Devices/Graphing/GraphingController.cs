using UnityEngine;

namespace CLGraphing
{
    public class GraphingController : MonoBehaviour
    {
        GraphingCursor cursor;
        private void Awake()
        {
            cursor = GameObject.FindObjectOfType<GraphingCursor>();
        }

        int acceleration = 0;
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Equals)) Graphing.Zoom(2);
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Minus)) Graphing.Zoom(0.5f);

            if (cursor == null || !cursor.Attached) return;
            bool right = Input.GetKey(KeyCode.RightArrow);
            bool left = Input.GetKey(KeyCode.LeftArrow);
            if (right ^ left)
            {
                int change = Mathf.Max(1, Mathf.RoundToInt(0.025f * (acceleration * acceleration) * Time.deltaTime));
                acceleration++;
                if (left) change = -change;
                cursor.Move(change);
            }
            else acceleration = 0;
        }
    }
}