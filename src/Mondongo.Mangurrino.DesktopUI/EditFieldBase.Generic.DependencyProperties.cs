// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Generic.DependencyProperties.cs" company="OscarNET-SOFTware">
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

partial class EditFieldBase<TFieldValue>
{
    /// <summary>
    /// Identifies the <see cref="FieldValue" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldValueProperty =
        DependencyProperty.Register(
            name: nameof(FieldValue),
            propertyType: typeof(TFieldValue?),
            ownerType: typeof(EditFieldBase<TFieldValue>),
            typeMetadata: new FrameworkPropertyMetadata(
                defaultValue: default(TFieldValue?),
                flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                propertyChangedCallback: OnFieldValueChanged));
}
