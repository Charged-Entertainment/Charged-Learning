using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Puzzle;
using Components;
using Dialogs;

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

        PlaySequence(Seq1);
    }

    private void Seq1()
    {
        AddEntry("In the previous level, you learned that batteries function as an energy source or a ‘voltage source’. In your first circuit, you connected your battery to a resistor, which turns the supplied energy into heat. But why would we ever want that?");
        AddEntry("I guess it could warm you up? I don’t see that happening with this tiny battery though.");
        AddEntry("Instead, we typically use resistors to limit the current flowing into our main source of interest so that it doesn’t burn out. For example, we might want to power an LED which emits light. Like this one! Let me show you.");
        AddEntry("Wait… Who messed with my circuit??");
        AddEntry("We need to reconnect this LED to the circuit.");
        AddEntry("An LED, short for 'light-emitting diode', is a 'semiconductor' device.");
        AddEntry("You'll get very familiar with diodes and semiconductor in general in the near future, but for now, all you need to know is that an LED can emit light when electric current passes through it.").ended += () => Book.ShowEmpty();
        AddEntry("Before you connect the LED, you'll need to flip it.");
        AddEntry("LEDs are a 'polarized' component. So far, it hasn't been of any importance which terminals you connect to the battery's positive and negative sides.");
        AddEntry("LEDs have two terminals, positive (the longer one), also called an 'anode'.");
        AddEntry("And negative, also called an 'cathode'.");
        AddEntry("You can learn more about this in your handy book.");
        AddEntry("You can flip the LED by right-clicking, then choosing 'Flip horizontally' from the menu. You'll also find the keyboard shortcut there.");
        AddEntry("Now flip the LED, connect it, then flip the circuit breaker and watch the LED light up.").started += () => Handle<SpiceSharp.Simulations.IBiasingSimulation>(ref SimulationManager.simulationDone, sim =>
        {
            var current = new SpiceSharp.Simulations.RealPropertyExport(sim, led.ID(), "i").Value;
            var vdrop = new SpiceSharp.Simulations.RealPropertyExport(sim, led.ID(), "v").Value;

            // voltage drop across the diode approximately equal to the value actually gotten when measuring if connected correctly.
            if (System.Math.Abs(vdrop - 0.72212958501093D) <= 1e-3)
            {
                PlaySequence(Seq2);
                return true;
            }
            else
            {
                // handle all possible erros by the player, each calling a different entry sequence.
                SeqErrorExample(); //temp
            }
            return false;
        }, handler => SimulationManager.simulationDone -= handler);
    }

    private void SeqErrorExample()
    {
        AddEntry("You idiot!!");
        AddEntry("Just kidding :)");
    }

    private void Seq2()
    {
        AddEntry("Great job!");
        AddEntry("As you can see, the LED light indicates that 'current' is flowing through it.");
        AddEntry("We mentioned before that voltage essentially means that *each electron* passing through the specified two points will need to excert (lose/gain) a set amount of energy.");
        AddEntry("Which gives us a certain amount of energy per electron, or more precisely, per each large group of them; energy *(joules) per coloumb*.");
        AddEntry("That's nice an all, but it still doesn't tell us how *many* or how fast coloumbs (electrons) are moving in the circuit. And that's exactly what current means.");
        AddEntry("Current is the rate at which electrons flow past a point in a complete electrical circuit. Measured in coloumbs/sec, which we typically just called 'amps', short for 'ampere'.").ended += () => Book.ShowEmpty();
        AddEntry("Cool theories and all but why is the LED's light so weak?!");
        AddEntry("I think we should try measuring the current flowing in this circuit to see what exactly is going on.");
        AddEntry("As with all measuring, and like you did last time, we'll need the multimeter for that. Go ahead and grab it.").started += () =>
        {
            Dialog.Pause();
            Handle(ref Multimeter.created, () =>
            {
                Dialog.Continue();
                return true;
            }, handler => Multimeter.created -= handler);
        };
        AddEntry("Now, unlike last time when you connected the multimeter to measure voltage, connecting it to measure current can be a bit tricky.");
        AddEntry("I'll just help you connect it for now, and I'll explain to you later why we did it this way.").started += () =>
        {
            Dialog.Pause();
            var bPos = battery.Terminals.First(t => t.gameObject.name == "positive_terminal");
            var bPosOther = bPos.connectedTerminals.First();

            var multimeter = Multimeter.GetBody();
            var multimeterRed = Multimeter.GetRedProbe();
            var multimeterBlack = Multimeter.GetBlackProbe();
            WaitThenDo(1, () => bPos.Disconnect());
            WaitThenDo(2, () => multimeter.Move(Vector2.right * 2.5f));
            WaitThenDo(3, () =>
            {
                multimeterBlack.Move(new Vector2(-2.5f, 3.5f), false);
                multimeterRed.Move(new Vector2(-5f, 3.5f), false);
            });
            WaitThenDo(4, () => bPos.Connect(multimeterRed.Terminal));
            WaitThenDo(5, () => bPosOther.Connect(multimeterBlack.Terminal));
            WaitThenDo(7, () => Dialog.Continue());
        };
        AddEntry("Now switch the multimeter on, put it in current mode, and flip the circuit breaker to read the measurement.").started += () => Handle<SpiceSharp.Simulations.IBiasingSimulation>(ref SimulationManager.simulationDone, sim =>
        {
            var current = new SpiceSharp.Simulations.RealPropertyExport(sim, led.ID(), "i").Value;
            var vdrop = new SpiceSharp.Simulations.RealPropertyExport(sim, led.ID(), "v").Value;

            // voltage drop across the diode approximately equal to the value actually gotten when measuring if connected correctly.
            if (Utils.Approximately(vdrop, 0.72212958501093D))
            {
                PlaySequence(Seq3);
                return true;
            }
            else
            {
                // handle all possible erros by the player, each calling a different entry sequence.
                SeqErrorExample(); //temp
            }
            return false;
        }, handler => SimulationManager.simulationDone -= handler);
    }

    private void Seq3()
    {
        AddEntry("Oh! These kind of LEDs typically need a current of 10-30mA to operate normally.");
        AddEntry("As you can see, we are barely at its minimum threshold. We need to increase the current.");
        AddEntry("The current is too low because we have to much resistance in our circuit. This should make sense intuitavely since resistance 'slows down' current and causes too much energy to be turn into heat.");
        AddEntry("You might think removing the resistance will solve our problem, but I'd *highly* advice against that; too little resistance (zero in that case) would cause the LED to burn out, or worse, explode!");
        AddEntry("Instead we need to smartly add and remove resistances to/from the circuit to get to a suitable total resistance.");
        AddEntry("Adding resistance to the circuit can lower the resistance? Oh yes! You must think I'm crazy, but hear me out.");
        AddEntry("Resistances can be connected to each other (or any other component) in two ways: series, and parallel!");
        AddEntry("There's a lot to understand here, but the summary is that 'series' resistors (next to each other) have their values summed up and cause a higher total resistance.");
        AddEntry("'Parallel' resistors (on top of each other) have a total value that is less than the smallest resistor in the parallel bunch.");
        AddEntry("Of course all of this is dictated by physical laws, and you'll get to hear all about that soon enough. But for now, this page should contain all you need to properly operate this LED.").ended += () => Book.ShowEmpty();
        AddEntry("Try to increase the current a little bit using your new knowledge, I’ll let you experiment as you will.").started += () =>
        {
            var goal = new ComponentValueMeasuredGoal("Run a current of ~20mA through the LED.", led.ID(), "i", 20 * 1e-3);
            Puzzle.AddGoal(goal);
            Handle<Goal>(ref Puzzle.goalAchieved, g =>
            {
                if (g == goal) PlaySequence(Seq4);
                return g == goal;
            }, handler => Puzzle.goalAchieved -= handler);
        };
    }

    private void Seq4()
    {
        AddEntry("Great job!!");
        AddEntry("What we have here is a circuit that combines series and parallel resistors in order to achieve our desired total resistance value.");
        AddEntry("Of course there are many other combinations that could be possible using these three resistors, all resulting in different total resistance values.");
        AddEntry("You could also directly use one resistor directly with the value you need, though you'll often find that you don't have it on hand, and it's more practical to combine the ones you already have than go out and purshase a new one with a specific value.");
        AddEntry("Now take a break, cool down, and come right back. We have tons more to learn before I can assign you real tasks we need for my next super secret project.");
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
