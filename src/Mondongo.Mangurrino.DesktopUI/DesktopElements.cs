// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DesktopElements.cs" company="OscarNET-SOFTware">
// ···
//      Mondongo.Dehesa.Framework - Just a set of essential libraries for DotNET: clean, simple and ready to use.
// ···
//      Copyright (c) 2025 Oscar Fernandez Gonzalez a.k.a. Osc@rNET
//      Licensed under the MIT License. See the 'LICENSE.md' file for details.
// ···
//      Third-party components are used in this project. For full license texts,
//      see the 'licenses' folder and the 'THIRD-PARTY-NOTICES.md' file.
// ···
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Mondongo.Mangurrino.DesktopUI;

/// <summary>
/// Provides various elements associated with the desktop.
/// </summary>
public sealed class DesktopElements
{
    /// <summary>
    /// Stores the list of key/value pair items representing all brushes.
    /// </summary>
    private static readonly List<KeyValuePair<string, Brush>> s_allBrushes = [
        new(nameof(Brushes.AliceBlue), Brushes.AliceBlue),
        new(nameof(Brushes.AntiqueWhite), Brushes.AntiqueWhite),
        new(nameof(Brushes.Aqua), Brushes.Aqua),
        new(nameof(Brushes.Aquamarine), Brushes.Aquamarine),
        new(nameof(Brushes.Azure), Brushes.Azure),
        new(nameof(Brushes.Beige), Brushes.Beige),
        new(nameof(Brushes.Bisque), Brushes.Bisque),
        new(nameof(Brushes.Black), Brushes.Black),
        new(nameof(Brushes.BlanchedAlmond), Brushes.BlanchedAlmond),
        new(nameof(Brushes.Blue), Brushes.Blue),
        new(nameof(Brushes.BlueViolet), Brushes.BlueViolet),
        new(nameof(Brushes.Brown), Brushes.Brown),
        new(nameof(Brushes.BurlyWood), Brushes.BurlyWood),
        new(nameof(Brushes.CadetBlue), Brushes.CadetBlue),
        new(nameof(Brushes.Chartreuse), Brushes.Chartreuse),
        new(nameof(Brushes.Chocolate), Brushes.Chocolate),
        new(nameof(Brushes.Coral), Brushes.Coral),
        new(nameof(Brushes.CornflowerBlue), Brushes.CornflowerBlue),
        new(nameof(Brushes.Cornsilk), Brushes.Cornsilk),
        new(nameof(Brushes.Crimson), Brushes.Crimson),
        new(nameof(Brushes.Cyan), Brushes.Cyan),
        new(nameof(Brushes.DarkBlue), Brushes.DarkBlue),
        new(nameof(Brushes.DarkCyan), Brushes.DarkCyan),
        new(nameof(Brushes.DarkGoldenrod), Brushes.DarkGoldenrod),
        new(nameof(Brushes.DarkGray), Brushes.DarkGray),
        new(nameof(Brushes.DarkGreen), Brushes.DarkGreen),
        new(nameof(Brushes.DarkKhaki), Brushes.DarkKhaki),
        new(nameof(Brushes.DarkMagenta), Brushes.DarkMagenta),
        new(nameof(Brushes.DarkOliveGreen), Brushes.DarkOliveGreen),
        new(nameof(Brushes.DarkOrange), Brushes.DarkOrange),
        new(nameof(Brushes.DarkOrchid), Brushes.DarkOrchid),
        new(nameof(Brushes.DarkRed), Brushes.DarkRed),
        new(nameof(Brushes.DarkSalmon), Brushes.DarkSalmon),
        new(nameof(Brushes.DarkSeaGreen), Brushes.DarkSeaGreen),
        new(nameof(Brushes.DarkSlateBlue), Brushes.DarkSlateBlue),
        new(nameof(Brushes.DarkSlateGray), Brushes.DarkSlateGray),
        new(nameof(Brushes.DarkTurquoise), Brushes.DarkTurquoise),
        new(nameof(Brushes.DarkViolet), Brushes.DarkViolet),
        new(nameof(Brushes.DeepPink), Brushes.DeepPink),
        new(nameof(Brushes.DeepSkyBlue), Brushes.DeepSkyBlue),
        new(nameof(Brushes.DimGray), Brushes.DimGray),
        new(nameof(Brushes.DodgerBlue), Brushes.DodgerBlue),
        new(nameof(Brushes.Firebrick), Brushes.Firebrick),
        new(nameof(Brushes.FloralWhite), Brushes.FloralWhite),
        new(nameof(Brushes.ForestGreen), Brushes.ForestGreen),
        new(nameof(Brushes.Fuchsia), Brushes.Fuchsia),
        new(nameof(Brushes.Gainsboro), Brushes.Gainsboro),
        new(nameof(Brushes.GhostWhite), Brushes.GhostWhite),
        new(nameof(Brushes.Gold), Brushes.Gold),
        new(nameof(Brushes.Goldenrod), Brushes.Goldenrod),
        new(nameof(Brushes.Gray), Brushes.Gray),
        new(nameof(Brushes.Green), Brushes.Green),
        new(nameof(Brushes.GreenYellow), Brushes.GreenYellow),
        new(nameof(Brushes.Honeydew), Brushes.Honeydew),
        new(nameof(Brushes.HotPink), Brushes.HotPink),
        new(nameof(Brushes.IndianRed), Brushes.IndianRed),
        new(nameof(Brushes.Indigo), Brushes.Indigo),
        new(nameof(Brushes.Ivory), Brushes.Ivory),
        new(nameof(Brushes.Khaki), Brushes.Khaki),
        new(nameof(Brushes.Lavender), Brushes.Lavender),
        new(nameof(Brushes.LavenderBlush), Brushes.LavenderBlush),
        new(nameof(Brushes.LawnGreen), Brushes.LawnGreen),
        new(nameof(Brushes.LemonChiffon), Brushes.LemonChiffon),
        new(nameof(Brushes.LightBlue), Brushes.LightBlue),
        new(nameof(Brushes.LightCoral), Brushes.LightCoral),
        new(nameof(Brushes.LightCyan), Brushes.LightCyan),
        new(nameof(Brushes.LightGoldenrodYellow), Brushes.LightGoldenrodYellow),
        new(nameof(Brushes.LightGray), Brushes.LightGray),
        new(nameof(Brushes.LightGreen), Brushes.LightGreen),
        new(nameof(Brushes.LightPink), Brushes.LightPink),
        new(nameof(Brushes.LightSalmon), Brushes.LightSalmon),
        new(nameof(Brushes.LightSeaGreen), Brushes.LightSeaGreen),
        new(nameof(Brushes.LightSkyBlue), Brushes.LightSkyBlue),
        new(nameof(Brushes.LightSlateGray), Brushes.LightSlateGray),
        new(nameof(Brushes.LightSteelBlue), Brushes.LightSteelBlue),
        new(nameof(Brushes.LightYellow), Brushes.LightYellow),
        new(nameof(Brushes.Lime), Brushes.Lime),
        new(nameof(Brushes.LimeGreen), Brushes.LimeGreen),
        new(nameof(Brushes.Linen), Brushes.Linen),
        new(nameof(Brushes.Magenta), Brushes.Magenta),
        new(nameof(Brushes.Maroon), Brushes.Maroon),
        new(nameof(Brushes.MediumAquamarine), Brushes.MediumAquamarine),
        new(nameof(Brushes.MediumBlue), Brushes.MediumBlue),
        new(nameof(Brushes.MediumOrchid), Brushes.MediumOrchid),
        new(nameof(Brushes.MediumPurple), Brushes.MediumPurple),
        new(nameof(Brushes.MediumSeaGreen), Brushes.MediumSeaGreen),
        new(nameof(Brushes.MediumSlateBlue), Brushes.MediumSlateBlue),
        new(nameof(Brushes.MediumSpringGreen), Brushes.MediumSpringGreen),
        new(nameof(Brushes.MediumTurquoise), Brushes.MediumTurquoise),
        new(nameof(Brushes.MediumVioletRed), Brushes.MediumVioletRed),
        new(nameof(Brushes.MidnightBlue), Brushes.MidnightBlue),
        new(nameof(Brushes.MintCream), Brushes.MintCream),
        new(nameof(Brushes.MistyRose), Brushes.MistyRose),
        new(nameof(Brushes.Moccasin), Brushes.Moccasin),
        new(nameof(Brushes.NavajoWhite), Brushes.NavajoWhite),
        new(nameof(Brushes.Navy), Brushes.Navy),
        new(nameof(Brushes.OldLace), Brushes.OldLace),
        new(nameof(Brushes.Olive), Brushes.Olive),
        new(nameof(Brushes.OliveDrab), Brushes.OliveDrab),
        new(nameof(Brushes.Orange), Brushes.Orange),
        new(nameof(Brushes.OrangeRed), Brushes.OrangeRed),
        new(nameof(Brushes.Orchid), Brushes.Orchid),
        new(nameof(Brushes.PaleGoldenrod), Brushes.PaleGoldenrod),
        new(nameof(Brushes.PaleGreen), Brushes.PaleGreen),
        new(nameof(Brushes.PaleTurquoise), Brushes.PaleTurquoise),
        new(nameof(Brushes.PaleVioletRed), Brushes.PaleVioletRed),
        new(nameof(Brushes.PapayaWhip), Brushes.PapayaWhip),
        new(nameof(Brushes.PeachPuff), Brushes.PeachPuff),
        new(nameof(Brushes.Peru), Brushes.Peru),
        new(nameof(Brushes.Pink), Brushes.Pink),
        new(nameof(Brushes.Plum), Brushes.Plum),
        new(nameof(Brushes.PowderBlue), Brushes.PowderBlue),
        new(nameof(Brushes.Purple), Brushes.Purple),
        new(nameof(Brushes.Red), Brushes.Red),
        new(nameof(Brushes.RosyBrown), Brushes.RosyBrown),
        new(nameof(Brushes.RoyalBlue), Brushes.RoyalBlue),
        new(nameof(Brushes.SaddleBrown), Brushes.SaddleBrown),
        new(nameof(Brushes.Salmon), Brushes.Salmon),
        new(nameof(Brushes.SandyBrown), Brushes.SandyBrown),
        new(nameof(Brushes.SeaGreen), Brushes.SeaGreen),
        new(nameof(Brushes.SeaShell), Brushes.SeaShell),
        new(nameof(Brushes.Sienna), Brushes.Sienna),
        new(nameof(Brushes.Silver), Brushes.Silver),
        new(nameof(Brushes.SkyBlue), Brushes.SkyBlue),
        new(nameof(Brushes.SlateBlue), Brushes.SlateBlue),
        new(nameof(Brushes.SlateGray), Brushes.SlateGray),
        new(nameof(Brushes.Snow), Brushes.Snow),
        new(nameof(Brushes.SpringGreen), Brushes.SpringGreen),
        new(nameof(Brushes.SteelBlue), Brushes.SteelBlue),
        new(nameof(Brushes.Tan), Brushes.Tan),
        new(nameof(Brushes.Teal), Brushes.Teal),
        new(nameof(Brushes.Thistle), Brushes.Thistle),
        new(nameof(Brushes.Tomato), Brushes.Tomato),
        new(nameof(Brushes.Transparent), Brushes.Transparent),
        new(nameof(Brushes.Turquoise), Brushes.Turquoise),
        new(nameof(Brushes.Violet), Brushes.Violet),
        new(nameof(Brushes.Wheat), Brushes.Wheat),
        new(nameof(Brushes.White), Brushes.White),
        new(nameof(Brushes.WhiteSmoke), Brushes.WhiteSmoke),
        new(nameof(Brushes.Yellow), Brushes.Yellow),
        new(nameof(Brushes.YellowGreen), Brushes.YellowGreen)
    ];

    /// <summary>
    /// Gets a read-only collection of key/value pair items representing all brushes.
    /// </summary>
    /// <value>
    /// The read-only collection with all brushes.
    /// </value>
    public static ReadOnlyCollection<KeyValuePair<string, Brush>> AllBrushes => s_allBrushes.AsReadOnly();
}
