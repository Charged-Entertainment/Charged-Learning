using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
public class Resistor : MonoBehaviour
{
    public enum ColorBand { Black, Brown, Red, Orange, Yellow, Green, Blue, Violet, Grey, White, Gold = -1, Silver = -2 }
    public class ColorBandSet
    {
        public ColorBand band1, band2, multiplier, tolerance;
        public ColorBandSet(ColorBand b1, ColorBand b2, ColorBand multiplier, ColorBand tolerance = ColorBand.Gold)
        {
            this.band1 = b1;
            this.band2 = b2;
            this.multiplier = multiplier;
            this.tolerance = tolerance;
        }
    }
    SpriteRenderer band1, band2, multiplierBand, toleranceBand;

    private void Awake()
    {
        band1 = transform.Find("b1").GetComponent<SpriteRenderer>();
        band2 = transform.Find("b2").GetComponent<SpriteRenderer>();
        multiplierBand = transform.Find("b3").GetComponent<SpriteRenderer>();
        toleranceBand = transform.Find("b4").GetComponent<SpriteRenderer>();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PuzzleScene") {
            gameObject.AddComponent<ResistorPuzzleSceneBehavior>();
        }
    }

    // #region testing
    // // temporary block just to test
    // [SerializeField] ulong testResistance = 0;
    // private void Update() {
    //     SetColorBands(testResistance);
    // }
    // #endregion

    public static readonly Dictionary<ColorBand, Color> ColorBandToColor = new Dictionary<ColorBand, Color>() {
        {ColorBand.Black, Color.black},
        {ColorBand.Brown, new Color(153/255f, 102/255f, 51/255f)},
        {ColorBand.Red, Color.red},
        {ColorBand.Orange, new Color(255f/255f, 153/255f, 0)},
        {ColorBand.Yellow, Color.yellow},
        {ColorBand.Green, Color.green},
        {ColorBand.Blue, Color.blue},
        {ColorBand.Violet, new Color(255f/255f, 0, 255f/255f)},
        {ColorBand.Grey, Color.gray},
        {ColorBand.White, Color.white},
        {ColorBand.Gold, new Color(255f/255f, 215/255f, 0)},
        {ColorBand.Silver, new Color(192/255f, 192/255f, 192/255f)},
    };

    public void SetColorBands(ulong resistance)
    {
        var colors = ResistanceToColorBandSet(resistance);
        if (colors != null) SetColorBands(colors);
    }

    public void SetColorBands(ColorBandSet bandSet)
    {
        band1.color = ColorBandToColor[bandSet.band1];
        band2.color = ColorBandToColor[bandSet.band2];
        multiplierBand.color = ColorBandToColor[bandSet.multiplier];
        toleranceBand.color = ColorBandToColor[bandSet.tolerance];
    }

    /// <Summary>
    /// Guaranteed to return an array of length 3 with the color codes or null if impossible.
    /// </Summary>
    public static ColorBandSet ResistanceToColorBandSet(ulong resistance)
    {
        if (resistance > 99e9 || !Regex.IsMatch(resistance.ToString(), @"^[0-9][0-9]?0*$"))
        {
            Debug.Log($"Error; this value ({resistance}) cannot be represented with 4 color-bands.");
            return null;
        }

        ColorBand val1 = 0, val2 = 0, mult = 0;
        if (resistance < 10) val2 = ((ColorBand)resistance);
        else
        {
            string vals = resistance.ToString();
            val1 = ColorBandExtensions.FromChar(vals[0]);
            val2 = ColorBandExtensions.FromChar(vals[1]);
            if (resistance >= 100) mult = (ColorBand)(vals.Length - 2);
        }
        return new ColorBandSet(val1, val2, mult);
    }
    // todo: use this insted of regex
    public static string[] GetResistorColors(double resistanceOhms)
    {
        // Define the color codes for each digit
        string[] colorCodes = new string[] {
            "black", "brown", "red", "orange", "yellow",
            "green", "blue", "violet", "gray", "white"
        };
        
        // Calculate the exponent and significant digits of the resistance
        int exponent = (int)Math.Floor(Math.Log10(resistanceOhms));
        double significantDigits = resistanceOhms / Math.Pow(10, exponent);
        
        // Get the first two significant digits, rounded to the nearest integer
        int digit1 = (int)Math.Round(significantDigits / 10);
        int digit2 = (int)Math.Round(significantDigits % 10);
        
        // Determine the multiplier and tolerance based on the exponent
        int multiplier = exponent - 2;
        double tolerance = (resistanceOhms < 1000) ? 0.2 : 0.05;
        
        // Generate the color band codes for each digit and the multiplier
        string[] colorBands = new string[] {
            colorCodes[digit1], colorCodes[digit2], colorCodes[multiplier]
        };
        
        // // Add the tolerance band if necessary
        // if (tolerance > 0)
        // {
        //     colorBands[2] += " gold"; // 5% tolerance is indicated by a gold band
        //     colorBands[3] = "brown"; // Fourth band is always brown for tolerance
        // }
        
        return colorBands;
    }

    /// <Summary>
    /// Must be passed an array of length 3 or greater; indices 3 and above will be ignored.
    /// </Summary>
    public static ulong ColorBandSetToResistance(ColorBandSet bandSet)
    {
        // value = (colorA*10 + colorB) * 10^colorC
        int val1 = bandSet.band1.ToInt() * 10;
        int val2 = bandSet.band2.ToInt();
        float mult = Mathf.Pow(10, bandSet.multiplier.ToFloat());
        return (ulong)(val1 + val2) * (ulong)Mathf.Round(mult);

    }
}
public static class ColorBandExtensions
{
    public static int ToInt(this Resistor.ColorBand b) { return (int)b; }
    public static float ToFloat(this Resistor.ColorBand b) { return (float)(((int)b)); }
    public static Resistor.ColorBand FromChar(char c) { return (Resistor.ColorBand)(int)(c - '0'); }
}

