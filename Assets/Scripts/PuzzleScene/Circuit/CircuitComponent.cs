
using Components;

public interface CircuitComponent{
    public Terminal[] Terminals{get;}

    public SpiceSharp.Entities.Entity[] GetSpiceComponent(string positiveWire, string negativeWire);

    public string ID();
}