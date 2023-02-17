using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Components{
        public enum PropertyType{
        Resistance,
        Voltage
    }

    public class PureProperty
    {
        // public string name;
        public PropertyType propertyType;
        // public Type valueType;
        // public object value;
        public double value;
        public int multiplier;
        public string unit;
        public bool isStatic;



        public PureProperty(PropertyType propertyType, double value, int multiplier, string unit, bool isStatic)
        {
            this.propertyType = propertyType;
            // this.valueType = valueType;
            this.value = value;
            this.multiplier = multiplier;
            this.unit = unit;
            this.isStatic = isStatic;
        }
    
    }

}