using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Manager : MonoBehaviour, IManager
{
    protected MainManager mainManager;
    protected MainController mainController;

    protected virtual void Awake() {
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
