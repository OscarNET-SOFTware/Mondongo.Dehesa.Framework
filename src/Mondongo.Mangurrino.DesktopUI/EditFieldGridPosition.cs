// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldGridPosition.cs" company="OscarNET-SOFTware">
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

using System.Windows;

namespace Mondongo.Mangurrino.DesktopUI;

/// <summary>
/// Represents the grid positioning and sizing configuration for label and field elements
/// within the <see cref="EditFieldBase" /> control.
/// </summary>
/// <remarks>
/// This struct groups together all the necessary grid coordinates and sizes, including
/// column and row indexes, their respective spans, and the width and height of the relevant
/// grid columns and rows. This allows for centralized management of positioning logic related
/// to the label and field in the control's layout.
/// </remarks>
public struct EditFieldGridPosition
{
    /// <summary>
    /// Stores the width of the first grid column.
    /// </summary>
    public GridLength Col0Width;

    /// <summary>
    /// Stores the width of the second grid column.
    /// </summary>
    public GridLength Col1Width;

    /// <summary>
    /// Stores the column index for the field element within the grid.
    /// </summary>
    public int FieldColumn;

    /// <summary>
    /// Stores the number of columns the field element spans.
    /// </summary>
    public int FieldColumnSpan;

    /// <summary>
    /// Stores the row index for the field element within the grid.
    /// </summary>
    public int FieldRow;

    /// <summary>
    /// Stores the number of rows the field element spans.
    /// </summary>
    public int FieldRowSpan;

    /// <summary>
    /// Stores the column index for the label element within the grid.
    /// </summary>
    public int LabelColumn;

    /// <summary>
    /// Stores the number of columns the label element spans.
    /// </summary>
    public int LabelColumnSpan;

    /// <summary>
    /// Stores the row index for the label element within the grid.
    /// </summary>
    public int LabelRow;

    /// <summary>
    /// Stores the number of rows the label element spans.
    /// </summary>
    public int LabelRowSpan;

    /// <summary>
    /// Stores the height of the first grid row.
    /// </summary>
    public GridLength Row0Height;

    /// <summary>
    /// Stores the height of the second grid row.
    /// </summary>
    public GridLength Row1Height;
}
