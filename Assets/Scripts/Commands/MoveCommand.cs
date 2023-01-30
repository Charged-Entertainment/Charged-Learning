using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private List<ComponentBehavior> _components;
    private Vector2 _position;
    public MoveCommand(List<ComponentBehavior> components, Vector2 position){
        _components = components;
        _position = position;
    }

    public void Execute(){
        foreach(var component in _components){
            component.Move(_position);
        }
    }

    public void Undo(){
        foreach(var component in _components){
            component.Move(-_position);
        }
    }
}
