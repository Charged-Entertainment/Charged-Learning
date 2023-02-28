using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorDeviceButton : MonoBehaviour
{
    [SerializeField] private char value;
    [SerializeField] private CalculatorController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponentInParent<CalculatorController>();
    }

    public void OnClick(){
        controller.WriteChar(value);
    }

    public void Solve(){
        controller.Submit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
