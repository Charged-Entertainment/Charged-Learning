using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{

    public class PureProperty
    {
        public string name;
        public Type type;
        public object value;
        public int multiplier;
        public string unit;
        public bool isStatic;

        public PureProperty(string name, Type type, object value, int multiplier, string unit, bool isStatic)
        {
            this.name = name;
            this.type = type;
            this.value = value;
            this.multiplier = multiplier;
            this.unit = unit;
            this.isStatic = isStatic;
        }
    }

    public class Component
    {

        public Dictionary<string, PureProperty> Properties { get; private set; }
        public string Name { get; private set; }

        public Component(){
            Properties = new Dictionary<string, PureProperty>();
        }
        // TODO: terminals
    }
}