using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using static Puzzle;
using SpiceSharp;
using SpiceSharp.Simulations;
using GameManagement;

public class Level3 : Tutorial
{
    #region level resources
    int lowRes = 230, midRes = 440, highRes = 700;
    double targetCurrent = 0.0187932469422333D;
    LevelComponent batteryLevelComponent, lowResistorLevelComponent, medResistorLevelComponent, highResistorLevelComponent, ledLevelComponent;
    LiveComponent battery, led, lowResistor, medResistor, highResistor;
    #endregion

    private void Start()
    {
        CreateLevelComponents();

        SetUpPuzzleInitialState();

        PlaySequence(Seq1);
    }
    private void CreateLevelComponents()
    {
        lowResistorLevelComponent = CreateLevelComponent("resistor", ComponentType.Resistor, 1);
        medResistorLevelComponent = CreateLevelComponent("resistor", ComponentType.Resistor, 1);
        highResistorLevelComponent = CreateLevelComponent("resistor", ComponentType.Resistor, 1);
        batteryLevelComponent = CreateLevelComponent("battery", ComponentType.Battery, 1);
        ledLevelComponent = CreateLevelComponent("led", ComponentType.Led, 1);
        AddProperty(lowResistorLevelComponent, PropertyType.Resistance, lowRes);
        AddProperty(medResistorLevelComponent, PropertyType.Resistance, midRes);
        AddProperty(highResistorLevelComponent, PropertyType.Resistance, highRes);
        AddProperty(batteryLevelComponent, PropertyType.Voltage, 9);

    }

    private void SetUpPuzzleInitialState()
    {
        led = LiveComponent.Instantiate(ledLevelComponent);
        battery = LiveComponent.Instantiate(batteryLevelComponent, pos: Vector2.down * 4);
        lowResistor = LiveComponent.Instantiate(lowResistorLevelComponent, pos: new Vector2(1, -1) * 4);
        medResistor = LiveComponent.Instantiate(medResistorLevelComponent, pos: new Vector2(1, 0) * 4);
        highResistor = LiveComponent.Instantiate(highResistorLevelComponent, pos: new Vector2(1, 1) * 4);
    }

    void Seq1()
    {
        AddEntry("Onto our next order of business.");
        AddEntry("LED’s are pretty cool, but like you’ve seen briefly in the last level, they have a few operating conditions:");
        AddEntry("1. They need to be connected to a power source with at least their minimum ‘forward voltage’ which depends on the type and model of the LED.");
        AddEntry("2. The need to be supplied a current within their operational range. More than that and they’ll burn (or even explode), less than that and they will barely light-up (or won’t light up at all).");
        AddEntry("3. They’re ‘polarized’, in that they’re the first component you see whose terminals are not interchangeable. An LED, when connected in reverse, practically operates as an open switch.");
        AddEntry("Like last time, fulfilling these three operating conditions for the LED is your goal for this level.");
        AddEntry("Hey, don’t worry, let’s take it apart together");
        AddEntry("Okay, let’s see…");
        AddEntry("For the first condition, it don’t see many batteries laying around anyways, this one battery must be all we need. (Things won’t be that easy in the future %wink%).");
        AddEntry("For the second condition, we’d normally go look at the datasheet in order to figure this out, but I’ll spare you this for now and I’ll do it for you. You’ll learn more about datasheets in due time.");
        AddEntry("LED datasheets are typically only provided to their manufacturers, because for the average use case, their operating conditions are pretty common knowledge and doesn’t really change based on their model.");
        AddEntry("This kind of common knowledge is a click of a button away in your favorite search engine, but who needs those when we have our handy little book here.").ended += Book.ShowEmpty;
        AddEntry("You can hover over the LED for a quick overview of its most important properties.");
        AddEntry("You saw this in the last level, and as expected, it should operate within 15mA-20mA.");
        AddEntry("What’s with all of this resistors though?! %looks nervous%");
        AddEntry("Just kidding, we just need to pick the right resistor for the job.");
        AddEntry("Last time you had three of the same kind, and you had to kind of guess and experiment until you obtained the optimal configuration.");
        AddEntry("Sadly, we can’t keep on guessing like that, since the wrong guess can, at best, damage components, or at worse, hurt us.");
        AddEntry("We need a more efficient, and rigorous way to determine these things. Thankfully, we have mathematicians and physicists to thank for providing us with convenient formulas we can use to do just that.");
        AddEntry("You’ll encounter many of these, starting with Ohm’s Law, which we’ll use in a minute.").ended += Book.ShowEmpty;
        AddEntry("But before we do that, we need to determine the values of these resistors.");
        AddEntry("Go ahead and measure them.");


        bool lowMeasured = false, midMeasured = false, highMeasured = false;
        Handle<double, PropertyType>(ref Multimeter.measured, (v, u) =>
        {
            if (u != PropertyType.Resistance) return false;
            if (v == lowRes) lowMeasured = true;
            if (v == midRes) midMeasured = true;
            if (v == highRes) highMeasured = true;
            if (lowMeasured && midMeasured && highMeasured)
            {
                PlaySequence(Seq2);
                return true;
            }
            return false;
        }, handler => Multimeter.measured -= handler);
    }

    void Seq2()
    {
        AddEntry("Boring right? Here’s how to know the values without measuring them.").ended += Book.ShowEmpty;
        AddEntry("You can practice this using the RCB Minigame which has now been unlocked! Feel free to try it out when we're done here.");
        AddEntry($"Hmmm, so, {lowRes} ohms, {midRes} ohms, and {highRes} ohms. Which one should we choose?");
        AddEntry("…");
        AddEntry("Alright alright, remember our secret formula, Ohm’s law.");
        AddEntry("We have a 9V battery, an LED that needs a current of roughly 20mA, and a few resistors that we need to choose from.");
        AddEntry("The formula is in you book in case you haven't checked it out yet.");
        AddEntry("Sorry to do this to you, but I need to go do something, why don’t you get a calculator and work on that until I come back.").started += () =>
        {
            Terminal.Enable();
            Calculator.Spawn();
            GameObject.FindObjectOfType<Calculator>().GetComponent<EditorBehaviour>().Move(new Vector2(-3, 0));

            var goal = new ComponentValueMeasuredGoal("Run a current of ~20mA through the LED.", led, "i", targetCurrent);
            Puzzle.AddGoal(goal);

            Handle<IBiasingSimulation>(ref SimulationManager.simulationDone, sim =>
            {
                var current = new RealPropertyExport(sim, led.ID(), "i").Value;
                if (Utils.Approximately(current, targetCurrent))
                {
                    PlaySequence(LastSeq);
                    return true;
                }
                else
                {
                    if (current < targetCurrent) PlaySequence(CurrentTooLowSeq);
                    else if (current > targetCurrent) PlaySequence(CurrentTooHighSeq);
                    else Debug.Log("Unknown error; this should never happen.");
                    doneWithNoHints = false;
                }
                return false;
            }, handler => SimulationManager.simulationDone -= handler);
        };
    }

    void CurrentTooLowSeq()
    {
        if (doneWithNoHints) AddEntry("Hey is everything goint okay?");
        AddEntry("Oh, seems like something’s wrong… The LED's too weak. I think we should use the multimeter to see what’s going on.");
    }

    void CurrentTooHighSeq()
    {
        if (doneWithNoHints) AddEntry("Hey is everything goint okay?");
        AddEntry("Oops!! That LED’s G.O.N.E. But I think our Advanced Simulation Technologies (AST) will allow us to still be able to use the multimeter to see what’s going on. Just don’t do this in real life because AST isn’t equipped to deal with flying shards of burnt, hot glass shards. %embarrassed expression%");
    }


    bool doneWithNoHints = true;
    void LastSeq()
    {
        if (doneWithNoHints) AddEntry("Hey do you need help with th.. Oh wow well done looks like you figured it out on your own. Great job!");
        else AddEntry("There you go, you're getting the hang of it!");
        AddEntry($"{(doneWithNoHints ? "You're getting better at this whole electronics thing, " : string.Empty)}I think you're ready for something a little bit more challenging.");
        AddEntry("Meet me here again in 5 minutes, I'll set something up for you.");
    }
}
