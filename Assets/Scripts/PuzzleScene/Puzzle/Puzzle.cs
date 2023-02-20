using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Puzzle : Singleton<Puzzle>
{
    public static Action<LevelComponent, Property> propertyRevealed { get; set; }
    public static Action<LevelComponent> quantityChanged { get; set; }
    public static Action<Goal> goalAchieved;
    public static Action goalsChanged;
    public static List<LevelComponent> Components { get; set; } = new List<LevelComponent>();
    public static List<Goal> Goals { get; private set; } = new List<Goal>();

    static public LevelComponent CreateLevelComponent(string name, ComponentType type, int qty)
    {
        var c = new Components.Component(type);
        var q = new Components.Quantity(qty);
        var t = new LevelComponent(c, q, name);
        Components.Add(t);
        return t;
    }

    static public PureProperty AddProperty(LevelComponent c, PropertyType type, float value, int multiplier, string unit, bool isStatic)
    {
        var p = new PureProperty(type, value, multiplier, unit, isStatic);
        c.AddProperty(p);
        return p;
    }

    static public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
        goalsChanged?.Invoke();
    }

    static public void AddGoals(List<Goal> goals){
        Goals.AddRange(goals);
        goalsChanged?.Invoke();
    }
}
