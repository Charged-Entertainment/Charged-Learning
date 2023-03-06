using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Puzzle;
using Components;
using static Dialogs.Dialog;
using Sequence = Dialogs.DialogSequence;
using Entry = Dialogs.DialogEntry;

public class Level2 : Tutorial
{
    #region level resources
    LevelComponent batteryLevelComponent, resistorLevelComponent, ledLevelComponent;
    LiveComponent battery, led, resistor1;
    #endregion

    private void Start()
    {
        CreateLevelComponents();

        SetUpPuzzleInitialState();

        Seq1();
    }

    private void Seq1()
    {
        AddEntry("Onto our next order of business.");
        AddEntry("Wait… Who messed with my circuit??");
        AddEntry("We need to reconnect this LED to the circuit.");
        AddEntry("An LED, short for 'light-emitting diode', is a 'semiconductor' device.");

        AddEntry("You'll get very familiar with diodes and semiconductor in general in the near future, but for now, all you need to know is that an LED can emit light when electric current passes through it.").ended += () => Book.ShowEmpty();

        AddEntry("Before you connect the LED, you'll need to flip it.");
        AddEntry("LEDs are a 'polarized' component. So far, it hasn't been the important which terminals you connect to the battery's positive and negative sides.");
        AddEntry("LEDs have two terminals, positive (the longer one), also called an 'anode'.");
        AddEntry("And negative, also called an 'cathode'.");
        AddEntry("You can learn more about this in your handy book.");
        AddEntry("You can flip the LED by right-clicking, then choosing 'Flip horizontally' from the menu. You'll also find the keyboard shortcut there.");
        AddEntry("Now flip the LED, connect it, then flip the circuit breaker and watch the LED light up.").started += () => Handle<SpiceSharp.Simulations.IBiasingSimulation>(ref SimulationManager.simulationDone, sim =>
        {
            var current = new SpiceSharp.Simulations.RealPropertyExport(sim, led.gameObject.GetInstanceID().ToString(), "i").Value;
            var vdrop = new SpiceSharp.Simulations.RealPropertyExport(sim, led.gameObject.GetInstanceID().ToString(), "v").Value;

            // voltage drop across the diode approximately equal to the value actually gotten when measuring if connected correctly.
            if (System.Math.Abs(vdrop - 0.72212958501093D) <= 1e-3)
            {
                Seq2();
                return true;
            }
            else {
                // handle all possible erros by the player, each calling a different entry sequence.
                SeqErrorExample(); //temp
            }
            return false;
        }, handler => SimulationManager.simulationDone -= handler);

        PlaySequence();
    }

    private void SeqErrorExample() {
        AddEntry("You idiot!!");
        AddEntry("Just kidding :)");

        PlaySequence(); 
    }

    private void Seq2()
    {
        AddEntry("Great job!");
        AddEntry("As you can see, the LED light indicates that current is flowing through it.");

        PlaySequence();
    }

    private void CreateLevelComponents()
    {
        resistorLevelComponent = CreateLevelComponent("resistor", ComponentType.Resistor, 3);
        batteryLevelComponent = CreateLevelComponent("battery", ComponentType.Battery, 1);
        ledLevelComponent = CreateLevelComponent("led", ComponentType.Led, 1);
        AddProperty(resistorLevelComponent, PropertyType.Resistance, 620);
        AddProperty(batteryLevelComponent, PropertyType.Voltage, 9);
    }

    private void SetUpPuzzleInitialState()
    {
        battery = LiveComponent.Instantiate(batteryLevelComponent, pos: new Vector2(-6, 0));
        resistor1 = LiveComponent.Instantiate(resistorLevelComponent, pos: new Vector2(-1, -1.5f));
        led = LiveComponent.Instantiate(ledLevelComponent, pos: new Vector2(-1.3f, 2f));

        battery.Terminals[0].Connect(resistor1.Terminals[1]);
        battery.Rotate(-90);
        led.FlipH();
    }
}
