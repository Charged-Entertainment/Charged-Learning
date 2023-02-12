using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Camera : Singleton<Camera>
{
    private UnityEngine.Camera cam;
    static private float minZoom = 10f, maxZoom = 2f;
    static private float vBound, hBound;
    static private float targetZoom = 5f;
    static private float zoomFactor = 6f;
    static private float zoomLerpSpeed = 10;

    static private Bounds bounds; 

    private ZoomController zoomController;
    private PanController panController;

    void Start()
    {
        bounds = GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds;
        cam = UnityEngine.Camera.main;
        targetZoom = cam.orthographicSize;

        zoomController = gameObject.AddComponent<ZoomController>();
        panController = gameObject.AddComponent<PanController>();

        SetHeightAndWidth();
    }

    static public void Pan(Vector3 value, bool isOffset = true)
    {
        if (isOffset) Instance.cam.transform.position += value;
        else Instance.cam.transform.position = value;
        
        Instance.cam.transform.position = new Vector3(
            Mathf.Clamp(Instance.cam.transform.position.x, -bounds.extents.x + hBound, bounds.extents.x - hBound),
            Mathf.Clamp(Instance.cam.transform.position.y, -bounds.extents.y + vBound, bounds.extents.y - vBound),
            Instance.cam.transform.position.z
        );
    }
    static public void Zoom(float change)
    {
        targetZoom -= change * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
    }

    private void Update()
    {
        if (cam.orthographicSize != targetZoom) {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
            SetHeightAndWidth();
        }
    }

    private void SetHeightAndWidth() {
        vBound = cam.orthographicSize;
        hBound = vBound * cam.aspect;
        Pan(Vector3.zero); // manual zero-value pan to clamp position
    }
}
