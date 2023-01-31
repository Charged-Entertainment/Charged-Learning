using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Camera : Singleton<Camera>, IHasControls
{
    private UnityEngine.Camera cam;
    static private float targetZoom = 5f;
    static private float zoomFactor = 6f;
    static private float zoomLerpSpeed = 10;

    public List<Controller> Controllers { get; set; }

    void Start()
    {
        cam = UnityEngine.Camera.main;
        targetZoom = cam.orthographicSize;

        Controllers = new List<Controller>();
        Controllers.Add(gameObject.AddComponent<PanController>());
        Controllers.Add(gameObject.AddComponent<ZoomController>());
    }

    static public void Pan(Vector3 value, bool isOffset = true)
    {
        if (isOffset) Instance.cam.transform.position += value;
        else Instance.cam.transform.position = value;
    }
    static public void Zoom(float change)
    {
        targetZoom -= change * zoomFactor;
    }

    private void Update()
    {
        if (cam.orthographicSize != targetZoom)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}
