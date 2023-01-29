using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour, IController
{
    protected MainController mainController;
    protected MainManager mainManager;

    virtual protected void Awake() {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    public void Enable() {
        enabled = true;
    }

    public void Disable() {
        enabled = false;
    }
}
