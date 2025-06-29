// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NifValidatorRegex.cs" company="OscarNET-SOFTware">
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

using System.Text.RegularExpressions;

namespace Mondongo.Iberico.Validations;

/// <summary>
/// Provides several regular expressions to validate CIF, NIF and NIE.
/// </summary>
public static partial class NifValidatorRegex
{
    /// <summary>
    /// Gets the regular expression to validate CIF.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex" /> object that represents the regular expression to validate CIF.
    /// </returns>
    [GeneratedRegex(@"^[ABCDEFGHJKLMNPQRSUVW]\d{7}[0-9A-J]$")]
    public static partial Regex CifRegex();

    /// <summary>
    /// Gets the regular expression to validate NIE.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex" /> object that represents the regular expression to validate NIE.
    /// </returns>
    [GeneratedRegex(@"^[XYZ]\d{7}[A-Z]$")]
    public static partial Regex NieRegex();

    /// <summary>
    /// Gets the regular expression to validate NIF.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex" /> object that represents the regular expression to validate NIF.
    /// </returns>
    [GeneratedRegex(@"^\d{8}[A-Z]$")]
    public static partial Regex NifRegex();
}
