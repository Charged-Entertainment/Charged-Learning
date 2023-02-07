using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Puzzle : Singleton<Puzzle>
{
    public static Action<LevelComponent, Property> propertyRevealed {get; private set;}
    public static Action<LevelComponent> quantityChanged {get; private set;}

    public static List<LevelComponent> Components {get; private set;}
    public static LevelComponent testComp;

    protected override void Awake() {
        base.Awake();
        Components = new List<LevelComponent>();
    }

    private void Start() {
        var p1 = new Components.PureProperty("Resistance", typeof(float), 5, 0, "Ohm", true);
        var resistor = new Components.Component();
        resistor.Properties.Add(p1.name, p1);
        var levelResistorQty = new Components.Quantity(3);
        testComp = new LevelComponent(resistor, levelResistorQty);
        testComp.Name = "Resistor_5_Ohm";
        Components.Add(testComp);
    }
}
