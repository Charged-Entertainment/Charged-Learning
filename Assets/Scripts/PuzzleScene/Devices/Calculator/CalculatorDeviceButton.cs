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

    public void OnClick()
    {
        controller.WriteChar(value);
    }

    public void Solve()
    {
        controller.Submit();
    }

    public void Clear()
    {
        controller.Clear();
    }

    public void Delete()
    {
        controller.Delete();
    }

    public void Up()
    {
        controller.Up();
    }

    public void Down()
    {
        controller.Down();
    }

    public void Right()
    {
        controller.Right();
    }

    public void Left()
    {
        controller.Left();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
