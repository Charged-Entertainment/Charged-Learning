
using Components;

public interface CircuitComponent{
    public Terminal[] Terminals{get;}

    public SpiceSharp.Components.Component GetSpiceComponent(string positiveWire, string negativeWire);

    public string ID();
}