using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPan : MonoBehaviour
{
     [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;




    private void Update() {

        panCamera();
        
    }

    private void panCamera(){
        //save position when mouse is clicked 
        if(Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        //calculate distance between origin and drag position 
        if (Input.GetMouseButton(0)){
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            print("origin " + dragOrigin + " newPosition" + cam.ScreenToWorldPoint(Input.mousePosition) + " =difference " + difference); 

            //move the camer by that distance 
            cam.transform.position += difference;
        }

        //move the camer by that distance 
        
    }
}
