using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    /*
    Any operations that should be performed on selected objects will first query
    this class for whatever available selected objects it has, then perform the operation
    on them.
    */

    private Vector3 startPosition;
    public List<ComponentBehavior> selectedGameObjects { get; private set; }

    [SerializeField] private Transform selectionArea;
    [SerializeField] private Clipboard clipboard;

    void Awake()
    {
        selectedGameObjects = new List<ComponentBehavior>();
        
        selectionArea = Instantiate(selectionArea);
        selectionArea.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //0=Left click
        {
            selectionArea.gameObject.SetActive(true);
            startPosition = Utils.GetMouseWorldPosition();

        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Utils.GetMouseWorldPosition();
            Vector3 bottomLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
                );

            Vector3 topRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
                );
            selectionArea.position = bottomLeft;
            selectionArea.localScale = topRight - bottomLeft;
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionArea.gameObject.SetActive(false);
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.SetSelectedVisible(false);
            }
            selectedGameObjects.Clear();

            Collider2D[] collidedObjectsArray = Physics2D.OverlapAreaAll(startPosition, Utils.GetMouseWorldPosition());
            foreach (var collidedObject in collidedObjectsArray)
            {
                ComponentBehavior componentObject = collidedObject.GetComponent<ComponentBehavior>();//Should be something other than ObjectSnapping
                if (componentObject)
                {
                    selectedGameObjects.Add(componentObject);
                    componentObject.SetSelectedVisible(true);
                }
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.FlipH();
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl)) {
            clipboard.Copy(selectedGameObjects.ToArray());
        }

        else if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl)) {
            clipboard.Paste(Utils.GetMouseWorldPosition());
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.Move(new Vector3(-1f,0,0));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.Move(new Vector3(1f,0,0));
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.Move(new Vector3(0,1,0));
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.Move(new Vector3(0,-1,0));
            }
        }

        if (Input.GetKeyDown(KeyCode.H) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.FlipH();
            }
        }

        if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.FlipV();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.Rotate(45);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl)) {
            foreach (var selectedObject in selectedGameObjects)
            {
                selectedObject.Rotate(-45);
            }
        }
    }
}
