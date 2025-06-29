// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NifPerson.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Iberico.Shared.Enums;

/// <summary>
/// Defines the person (in legal meaning) who has a spanish tax identification number.
/// </summary>
public enum NifPerson
{
    /// <summary>
    /// Wrong legal person or entity (invalid spanish tax identification number).
    /// </summary>
    Wrong = 0,

    /// <summary>
    /// Natural person with spanish identity document.
    /// </summary>
    Nif = 1,

    /// <summary>
    /// Natural person with foreign-spanish identity document.
    /// </summary>
    Nie = 2,

    /// <summary>
    /// Juridical person that is a non-human legal entity, i.e. any organization.
    /// </summary>
    Cif = 3
}
