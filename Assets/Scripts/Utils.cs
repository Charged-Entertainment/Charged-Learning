using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Components;
using UnityEngine.UIElements;

public class Utils : MonoBehaviour
{
    public static Vector3 GetMouseWorldPosition()
    {
        return UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //TODO: generic version
    // public static Collider2D PhysicsOverlapAreaAll<T>(Vector2 PointA, Vector2 PointB)

    public static Bounds GetBounds<T>(IList<T> components) where T : EditorBehaviour
    {

        if (components.Count == 0)
            return new Bounds();

        Bounds bounds = new Bounds(components[0].transform.position, Vector3.zero);
        foreach (var cmp in components)
        {
            bounds.Encapsulate(cmp.GetBounds());
        }
        return bounds;
    }

    public static Terminal[] GetTerminals(EditorBehaviour c)
    {
        return c.gameObject.GetComponentsInChildren<Terminal>(true);
    }

    public static bool IsMouseOverGameObject()
    {
        var colliders = Physics2D.RaycastAll(Utils.GetMouseWorldPosition(), Vector2.zero);
        // print($"Mouse is over the following gameobjects [{colliders.Length}]: ");
        foreach (var collider in colliders) print(collider.transform.gameObject);
        return colliders.Length != 0;
    }

    public static bool IsMouseOverAnything()
    {
        return IsMouseOverGameObject() || UI.IsMouseOverUI();
    }

    public static Vector2 ScreenToPanelPosition(IPanel panel, Vector2 screenPosition){
        Vector2 mousePositionCorrected = new Vector2(screenPosition.x, Screen.height - screenPosition.y);
        mousePositionCorrected = RuntimePanelUtils.ScreenToPanel(panel, mousePositionCorrected);

        return mousePositionCorrected;
    }

    public class UI
    {
        public static bool IsMouseOverUI()
        {
            var t = GetAllUIUnderCursor();
            return !(t.Count == 1 && t[0].name == "game-area");
        }
        public static VisualElement GetUIUnderCursor()
        {
            List<RaycastResult> res = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current), res);
            var panel = res[0].gameObject.GetComponent<PanelEventHandler>().panel;

            Vector2 pointerScreenPos = Input.mousePosition;
            Vector2 pointerUiPos = new Vector2 { x = pointerScreenPos.x, y = Screen.height - pointerScreenPos.y };

            VisualElement picked = panel.Pick(RuntimePanelUtils.ScreenToPanel(panel, pointerUiPos));

            // print($"Picked the following [1]: {picked}");
            return picked;
        }

        public static List<VisualElement> GetAllUIUnderCursor()
        {
            List<RaycastResult> res = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current), res);
            var panel = res[0].gameObject.GetComponent<PanelEventHandler>().panel;
            List<VisualElement> picked = new List<VisualElement>();

            Vector2 pointerScreenPos = Input.mousePosition;
            Vector2 pointerUiPos = new Vector2 { x = pointerScreenPos.x, y = Screen.height - pointerScreenPos.y };

            panel.PickAll(RuntimePanelUtils.ScreenToPanel(panel, pointerUiPos), picked);

            // print($"Picked the following [{picked.Count}]: ");
            // foreach (var p in picked) print(p);

            return picked;
        }

    }
}