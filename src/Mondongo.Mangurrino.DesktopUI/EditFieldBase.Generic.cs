// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Generic.cs" company="OscarNET-SOFTware">
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
/// An abstract base class that provides functionality for field editing.
/// </summary>
/// <typeparam name="TFieldValue">The type of field value.</typeparam>
public abstract partial class EditFieldBase<TFieldValue> : EditFieldBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EditFieldBase{TFieldValue}" /> class.
    /// </summary>
    protected EditFieldBase()
        : base()
    {
    }
}
