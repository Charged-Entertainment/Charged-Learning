using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{

    public class Quantity
    {
        public int Total { get; private set; }
        public int Used { get; set; }

        public Quantity(int total, int used = 0)
        {
            this.Total = total;
            this.Used = used;
        }
    }
    public class Property
    {
        public bool isRevealed;
        public PureProperty pureProperty { get; private set; }

        public Property(PureProperty pureProperty, bool isRevealed = false)
        {
            this.pureProperty = pureProperty;
            this.isRevealed = isRevealed;
        }
    }

    public class LevelComponent
    {
        public Component Component { get; private set; }

        public static Action<LevelComponent> quantityChanged;

        public Dictionary<PropertyType, Property> Properties { get; private set; }

        // public Dictionary<string, Terminal> Terminals { get; private set; }

        public Quantity Quantity { get; private set; }

        public string Name { get; set; }



        public LevelComponent(Component component, Quantity quantity)
        {
            Properties = new Dictionary<PropertyType, Property>();
            this.Component = component;
            this.Quantity = quantity;

            foreach (var prop in component.Properties)
            {
                Properties.Add(prop.Value.propertyType, new Property(prop.Value));
            }
            LiveComponent.created += HandleComponentCreated;
            LiveComponent.destroyed += HandleComponentDestroyed;
        }

        // TODO: handle reveal event and qunatity change event
        private void HandleComponentCreated(LiveComponent comp)
        {
            if (comp.levelComponent == this)
            {
                if (Quantity.Used < Quantity.Total)
                {
                    Quantity.Used++;
                    quantityChanged.Invoke(this);
                }
                else
                {
                    throw new Exception("error");
                }

            }
        }

        // TODO: handle reveal event and qunatity change event
        private void HandleComponentDestroyed(EditorBehaviour comp)
        {
            if (comp.GetComponent<LiveComponent>().levelComponent == this)
            {
                if (Quantity.Used >= 1)
                {
                    Quantity.Used--;
                    quantityChanged.Invoke(this);
                }
                else
                {
                    Debug.Log(Quantity);
                    Debug.Log(Quantity.Used);
                    throw new Exception("error");
                }

            }
        }

        // public bool 
    }
}
