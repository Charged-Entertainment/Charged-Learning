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
        return colliders.Length != 0;
    }

    public static bool IsMouseOverAnything()
    {
        return IsMouseOverGameObject() || IsMouseOverUI();
    }

    public static Vector2 ScreenToPanelPosition(IPanel panel, Vector2 screenPosition)
    {
        Vector2 mousePositionCorrected = new Vector2(screenPosition.x, Screen.height - screenPosition.y);
        mousePositionCorrected = RuntimePanelUtils.ScreenToPanel(panel, mousePositionCorrected);
        return mousePositionCorrected;
    }

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    public static VisualElement GetUIUnderCursor()
    {
        var panel = UI.GetRootVisualElement().panel;
        Vector2 pointerScreenPos = Input.mousePosition;
        Vector2 pointerUiPos = new Vector2 { x = pointerScreenPos.x, y = Screen.height - pointerScreenPos.y };
        VisualElement picked = panel.Pick(RuntimePanelUtils.ScreenToPanel(panel, pointerUiPos));
        return picked;
    }

    public static bool Approximately(double a, double b) {
        return System.Math.Abs(a - b) <= 1e-3;
    }

    public static bool Approximately(Vector3 a, Vector3 b) {
        return Approximately(a.x, b.x) && Approximately(a.y, b.y) && Approximately(a.z, b.z); 
    }

    public static List<VisualElement> GetAllUIUnderCursor()
    {
        var panel = UI.GetRootVisualElement().panel;
        List<VisualElement> picked = new List<VisualElement>();
        Vector2 pointerScreenPos = Input.mousePosition;
        Vector2 pointerUiPos = new Vector2 { x = pointerScreenPos.x, y = Screen.height - pointerScreenPos.y };
        panel.PickAll(RuntimePanelUtils.ScreenToPanel(panel, pointerUiPos), picked);
        return picked;
    }
}