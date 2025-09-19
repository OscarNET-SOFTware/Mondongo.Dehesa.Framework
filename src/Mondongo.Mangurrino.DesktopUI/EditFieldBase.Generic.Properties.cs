// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Generic.Properties.cs" company="OscarNET-SOFTware">
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

partial class EditFieldBase<TFieldValue>
{
    /// <summary>
    /// Gets or sets the value of the field.
    /// </summary>
    /// <value>
    /// A <typeparamref name="TFieldValue" /> object that represents the value of the field.
    /// </value>
    public TFieldValue? FieldValue
    {
        get => (TFieldValue?)GetValue(FieldValueProperty);
        set => SetValue(FieldValueProperty, value);
    }
}
