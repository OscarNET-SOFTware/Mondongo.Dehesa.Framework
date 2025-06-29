// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NifValidator.cs" company="OscarNET-SOFTware">
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

using Mondongo.Iberico.Shared.Enums;

namespace Mondongo.Iberico.Validations;

/// <summary>
/// Provides several methods to validate CIF, NIF and NIE.
/// </summary>
public static class NifValidator
{
    /// <summary>
    /// Defines the CIF valid control letters.
    /// </summary>
    public const string CifValidControlLetters = "JABCDEFGHI";

    /// <summary>
    /// Defines the CIF valid initial letters.
    /// </summary>
    public const string CifValidInitialLetters = "PQRSNW";

    /// <summary>
    /// Defines the NIE and NIF valid letters.
    /// </summary>
    public const string NieAndNifValidLetters = "TRWAGMYFPDXBNJZSQVHLCKE";

    /// <summary>
    /// Check if a given string is a valid spanish tax identification number.
    /// </summary>
    /// <param name="value">A <see cref="string" /> object containing the value to be validated.</param>
    /// <param name="type">Indicates the <see cref="NifPerson" /> with which the <paramref name="value" /> will be validated.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="value" /> is a valid spanish tax identification number of <paramref name="type" /> ; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValid(string? value, NifPerson type) => type switch
    {
        NifPerson.Cif => IsValidCif(value),
        NifPerson.Nif => IsValidNif(value),
        NifPerson.Nie => IsValidNie(value),
        _ => false
    };

    /// <summary>
    /// Check if a given string is a valid CIF.
    /// </summary>
    /// <param name="cif">A <see cref="string" /> containing the hypothetical CIF to be validated.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="cif" /> is a valid CIF; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Although the term CIF has been obsolete since 2008, it is still used in practice to identify legal entities.
    /// The format consists of an initial letter indicating the entity type, seven numbers, and a control digit/letter.
    /// 
    /// Explanation of CIF Validation:
    /// 
    ///     Full CIF validation implements the official algorithm defined in Order EHA/451/2008:
    /// 
    ///         1. Basic Structure:
    ///             * 9-character length
    ///             * Initial letter indicating entity type (A, B, C, D, E, F, G, H, J, K, L, M, N, P, Q, R, S, U, V, W)
    ///             * Middle 7 digits
    ///             * Control character (digit or letter)
    /// 
    ///         2. Control digit calculation:
    ///             * Addition of digits in even positions (2nd, 4th, and 6th positions)
    ///             * For odd positions (1st, 3rd, 5th, and 7th):
    ///                 - Multiply digit by 2
    ///                 - Add individual digits of the result (e.g., 12 → 1 + 2 = 3)
    ///             * Total sum = even_sum + odd_sum
    ///             * Control digit = (10 - (total_sum % 10)) % 10
    /// 
    ///         3. Conversion to letter (for types P, Q, R, S, N, W):
    ///             * 0 → J | 1 → A | 2 → B | 3 → C | 4 → D
    ///             * 5 → E | 6 → F | 7 → G | 8 → H | 9 → I
    /// 
    ///         4. Final validation:
    ///             * For letter types: compare with calculated letter
    ///             * For other types: compare with calculated digit
    /// 
    /// This implementation complies with the requirements of the Spanish Tax Agency and correctly handles
    /// all special cases defined in current regulations.
    /// 
    /// * Examples: A80329113       Public limited companies.
    ///             B90735556       Limited liability companies.
    ///             C28102192       General partnerships.
    ///             D80161466       Limited partnerships.
    ///             E84117704       Communities of property.
    ///             F18415646       Cooperative societies.
    ///             G29938487       Associations and foundations.
    ///             H00778456       Communities of owners under horizontal property regime.
    ///             J62421516       Civil societies.
    /// 
    ///             K7549890T       Older formats that are no longer used
    ///             L0726882T
    ///             M8919873T
    ///             
    ///             N2769743B       Non-resident entities.
    ///             P4179833A       Local corporations.
    ///             Q0597652G       Autonomous bodies, whether state-owned or not, and assimilated.
    ///             R5201806F       Religious congregations and institutions.
    ///             S7919243A       Bodies of the State Administration and autonomous communities.
    ///             U32478307       Temporary Business Associations.
    ///             V52538097       Agrarian Transformation Society.
    ///             W4358578E       Permanent establishments of non-resident entities in Spain.
    /// 
    /// * More info: https://www.boe.es/buscar/act.php?id=BOE-A-2008-3580
    ///              https://es.wikipedia.org/wiki/C%C3%B3digo_de_identificaci%C3%B3n_fiscal
    /// </remarks>
    public static bool IsValidCif(string? cif)
    {
        if (ShouldNotBeValidatedAsCif(cif))
        {
            return false;
        }

        int total = CalculateCifControlDigit(cif!);
        int computedDigit = (10 - (total % 10)) % 10;
        return cif![8] == CalculateCifControlLetterOrNumber(cif, computedDigit);
    }

    /// <summary>
    /// Check if a given string is a valid NIE.
    /// </summary>
    /// <param name="nie">A <see cref="string" /> object containing the hypothetical NIE to be validated.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="nie" /> is a valid NIE; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// For foreign residents, the NIE begins with X, Y, or Z, followed by seven digits and a control letter.
    /// The initial letter is replaced by 0, 1, or 2 to calculate the letter, using the same algorithm as the NIF.
    ///
    /// * Examples: X7366701P / Y6827638X / Z5222533C
    ///
    /// * More info: https://www.boe.es/boe/dias/2008/07/15/pdfs/A30880-30880.pdf
    ///              https://es.wikipedia.org/wiki/N%C3%BAmero_de_identidad_de_extranjero
    /// </remarks>
    public static bool IsValidNie(string? nie)
    {
        if (ShouldNotBeValidatedAsNie(nie))
        {
            return false;
        }

        char initial = nie![0];
        string numbers = nie.Substring(1, 7);
        string nieNumber = (initial == 'X' ? "0" : initial == 'Y' ? "1" : "2") + numbers;
        int number = int.Parse(nieNumber);
        return nie[8] == NieAndNifValidLetters[number % 23];
    }

    /// <summary>
    /// Check if a given string is a valid NIF.
    /// </summary>
    /// <param name="nif">A <see cref="string" /> object containing the hypothetical NIF to be validated.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="nif" /> is a valid NIF; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// For Spanish individuals, the NIF consists of eight digits followed by a control letter,
    /// calculated by dividing the number by 23 and using the official table of letters.
    /// 
    /// * Examples: 14948439A / 47038024B / 15671258R / 82558060C / 08775860A
    ///
    /// * More info: https://www.boe.es/buscar/doc.php?id=BOE-A-2007-15984
    ///              https://es.wikipedia.org/wiki/N%C3%BAmero_de_identificaci%C3%B3n_fiscal
    /// </remarks>
    public static bool IsValidNif(string? nif)
    {
        if (ShouldNotBeValidateAsNif(nif))
        {
            return false;
        }

        int number = int.Parse(nif!.Substring(0, 8));
        return nif[8] == NieAndNifValidLetters[number % 23];
    }

    /// <summary>
    /// Calculates the control digit of a given CIF.
    /// </summary>
    /// <param name="cif">A <see cref="string" /> containing the CIF.</param>
    /// <returns>
    /// A <see cref="int" /> that represents the calculated control digit.
    /// </returns>
    private static int CalculateCifControlDigit(string cif)
    {
        string centralPart = cif.Substring(1, 7);

        int sumEven = 0;
        int sumOdd = 0;

        for (int i = 0; i < centralPart.Length; i++)
        {
            int digit = centralPart[i] - '0';

            if (i % 2 == 1)
            {
                sumEven += digit;
            }
            else
            {
                int doubled = digit * 2;
                sumOdd += doubled / 10 + doubled % 10;
            }
        }

        return sumEven + sumOdd;
    }

    /// <summary>
    /// Calculates the control letter or number of a given CIF.
    /// </summary>
    /// <param name="cif">A <see cref="string" /> containing the CIF.</param>
    /// <param name="computedDigit">The computed digit.</param>
    /// <returns>
    /// An <see cref="int" /> that represents the ASCII code of the calculated control letter or number.
    /// </returns>
    private static int CalculateCifControlLetterOrNumber(string cif, int computedDigit) =>
        CifValidInitialLetters.Contains(cif[0]) ? CifValidControlLetters[computedDigit] : (computedDigit + '0');

    /// <summary>
    /// Checks if a given string can be validated as CIF.
    /// </summary>
    /// <param name="cif">An <see cref="string" /> object containing a valid hypothetical CIF.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="cif" /> cannot be validated as CIF; otherwise, <c>false</c>.
    /// </returns>
    private static bool ShouldNotBeValidatedAsCif(string? cif) =>
        string.IsNullOrWhiteSpace(cif) || (cif.Length != 9) || !NifValidatorRegex.CifRegex().IsMatch(cif);

    /// <summary>
    /// Checks if a given string can be validated as NIE.
    /// </summary>
    /// <param name="nie">An <see cref="string" /> object containing a valid hypothetical NIE.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="nie" /> cannot be validated as NIE; otherwise, <c>false</c>.
    /// </returns>
    private static bool ShouldNotBeValidatedAsNie(string? nie) =>
        string.IsNullOrWhiteSpace(nie) || !NifValidatorRegex.NieRegex().IsMatch(nie);

    /// <summary>
    /// Checks if a given string can be validated as NIF.
    /// </summary>
    /// <param name="nif">An <see cref="string" /> object containing a valid hypothetical NIF.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="nif" /> cannot be validated as NIF; otherwise, <c>false</c>.
    /// </returns>
    private static bool ShouldNotBeValidateAsNif(string? nif) =>
        string.IsNullOrWhiteSpace(nif) || !NifValidatorRegex.NifRegex().IsMatch(nif);
}
