using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour, IController
{
    protected MainController mainController;
    protected MainManager mainManager;

    private void Awake() {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
