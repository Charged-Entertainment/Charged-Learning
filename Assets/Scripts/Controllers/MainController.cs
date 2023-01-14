using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : Controller
{
    private void Awake() {
        gameObject.AddComponent<ClipboardController>();
        gameObject.AddComponent<SelectionController>();
        gameObject.AddComponent<TransformationController>();
    }
    
    // TODO: add public (generic?) methods to enable and disable managers.
    // public void EnableManager<T>() {

    // }

    // public void DisableManager<T>() {

    // }
}
