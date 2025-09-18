// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldLabelPosition.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Mangurrino.DesktopUI;

/// <summary>
/// Specifies the position of the label relative to the field.
/// </summary>
public enum EditFieldLabelPosition
{
    /// <summary>
    /// Label placed to the left of the field.
    /// </summary>
    Left = 0,

    /// <summary>
    /// Label placed at the top of the field.
    /// </summary>
    Top = 1,

    /// <summary>
    /// Label placed to the right of the field.
    /// </summary>
    Right = 2,

    /// <summary>
    /// Label placed at the bottom of the field.
    /// </summary>
    Bottom = 3,

    /// <summary>
    /// No label displayed.
    /// </summary>
    None = 4
}
