using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Puzzle : Singleton<Puzzle>
{
    public static Action<LevelComponent, Property> propertyRevealed { get; set; }
    public static Action<LevelComponent> quantityChanged { get; set; }

    public static List<LevelComponent> Components { get; set; }
    protected override void Awake()
    {
        base.Awake();
        Components = new List<LevelComponent>();

        // test resistor
        var c1 = new Components.Component();
        var q1 = new Components.Quantity(5);
        var t1 = new LevelComponent(c1, q1);
        t1.Name = "resistor";
        Components.Add(t1);

        // test battery
        var c2 = new Components.Component();
        var q2 = new Components.Quantity(1);
        var t2 = new LevelComponent(c2, q2);
        t2.Name = "battery";
        Components.Add(t2);

        // test LED
        var c3 = new Components.Component();
        var q3 = new Components.Quantity(2);
        var t3 = new LevelComponent(c3, q3);
        t3.Name = "led";
        Components.Add(t3);
    }
}
