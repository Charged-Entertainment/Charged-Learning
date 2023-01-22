using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Manager : MonoBehaviour, IManager
{
    protected MainManager mainManager;

    protected virtual void Awake() {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }
    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
