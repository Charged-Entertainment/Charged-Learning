using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{

    public enum ComponentType{
        Resistor,
        Battery,
        Led,
        Capacitor
    }

    public class Component
    {
        public ComponentType componentType{get; private set;}
        public string Name { get; private set; }
        public Dictionary<PropertyType, PureProperty> Properties { get; private set; }

        public Component(ComponentType componentType){
            this.componentType = componentType;
            Properties = new Dictionary<PropertyType, PureProperty>();
        }

        public void AddProperty(PureProperty property){
            Properties.Add(property.propertyType, property);
        }

        // TODO: terminals
    }
}