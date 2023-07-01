using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
/// <summary>Alternative to enums that represents a color by its hex code</summary>
public class PaletteColor
{
    public string Color { get; private set; }
    public PaletteColor(string color) { Color = color; }
    public static PaletteColor Red { get { return new PaletteColor("#FF0000"); } }
    public static PaletteColor Green { get { return new PaletteColor("#00FF00"); } }

    public override string ToString()
    {
        return Color;
    }
    //...
}

public class RichText
{
    public string HTML { get; private set; }
    /// <summary>Uses Regex to parse the text</summary>
    public string Text { get; private set; }

    RichText(string text)
    {
        Text = Regex.Replace(text, "<.*?>", string.Empty);
        HTML = text;
    }

    public override string ToString()
    {
        return HTML;
    }

    public static implicit operator string(RichText t)
    {
        return t.HTML;
    }

    public static implicit operator RichText(string str)
    {
        return new RichText(str);
    }

    public static RichText Italic(RichText text)
    {
        return new RichText($"<i>{text}</i>");
    }

    public static RichText Color(RichText text, PaletteColor color)
    {
        return new RichText($"<color={color}>{text}</color>");
    }

    public static RichText Color(RichText text, string color)
    {
        return new RichText($"<color={color}>{text}</color>");
    }

    public static RichText Bold(RichText text)
    {
        return new RichText($"<b>{text}</b>");
    }

    public static RichText StrikeThrough(RichText text){
        return new RichText($"<s>{text}</s>");
    }
    //... more tags can be added


}
