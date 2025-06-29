// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NifValidatorTests.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Iberico.Validations.Tests;

public sealed class NifValidatorTests
{
    public static readonly TheoryData<string?, NifPerson> s_validTaxIdentificationNumberData = new()
    {
        { "A09884776", NifPerson.Cif },
        { "B52469723", NifPerson.Cif },
        { "C78242054", NifPerson.Cif },
        { "X2446644L", NifPerson.Nie },
        { "Y5494627X", NifPerson.Nie },
        { "Z0972822L", NifPerson.Nie },
        { "43391849A", NifPerson.Nif },
        { "62840128B", NifPerson.Nif },
        { "01604017C", NifPerson.Nif }
    };

    public static readonly TheoryData<string?, NifPerson> s_wrongTaxIdentificationNumberData = new()
    {
        { "A09884776", NifPerson.Nie },
        { "B52469723", NifPerson.Nie },
        { "C78242054", NifPerson.Nie },
        { "X2446644L", NifPerson.Nif },
        { "Y5494627X", NifPerson.Nif },
        { "Z0972822L", NifPerson.Nif },
        { "43391849A", NifPerson.Cif },
        { "62840128B", NifPerson.Cif },
        { "01604017C", NifPerson.Cif }
    };

    public static readonly TheoryData<string?> s_validCifData = new()
    {
        { "A28854289" },    // Public limited company (real example: Empresa Municipal de Transportes de Fuenlabrada SA)
        { "B16713117" },    // Limited liability company (real example: Garmeltec SL [GMT-Motors])
        { "C06774848" },    // General partnership (valid format / non-real example)
        { "D63216956" },    // Limited partnership (valid format / non-real example)
        { "E14069918" },    // Community of property (valid format / non-real example)
        { "F06011035" },    // Cooperative society (real example: Sta. Maria Magdalena de Solana de los Barros S.Coop.Ltda)
        { "G80026362" },    // Association (real example: AA.VV. Mancomunidad General de El Naranjo, Fuenlabrada)
        { "H55155659" },    // Community of owners under horizontal property regime (valid format / non-real example)
        { "J20831632" },    // Civil Society (valid format / non-real example)
        { "N0071290A" },    // Non-resident entity (real example: Microsoft Ireland Operations Ltd)
        { "P0612600G" },    // Local corporation (real example: Ayto. Solana de los Barros)
        { "Q2801276C" },    // Public body (real example: Hospital de Fuenlabrada)
        { "R2800681E" },    // Religious institution (real example: Colegio Salesianos Atocha)
        { "S7800001E" },    // Autonomous community (real example: Comunidad de Madrid)
        { "U16808792" },    // Temporary union of companies (real example: Avanza Movilidad Integral SLU y Viguesa de Transportes SL UTE)
        { "V41146317" },    // Agrarian Transformation Society (real example: Sociedad Agraria de Transformacion 5800 Alia Sat)
        { "W0041692E" }     // Permanent establishment of a non-resident entity (real example: Cytiva Europe Gmbh Sucursal en España (Extinguida))
    };

    public static readonly TheoryData<string?> s_wrongCifData = new()
    {
        { null },           // This CIF is null.
        { "" },             // This CIF is empty.
        { "   "},           // This CIF only has blank spaces.
        { "A288542899" },   // All of the following CIF have an extra invalid length.
        { "B167131177" },
        { "C067748488" },
        { "A28854280" },    // All of the following CIF have an invalid control digit or a wrong control letter.
        { "B16713110" },
        { "C06774840" },
        { "D63216950" },
        { "E14069910" },
        { "F06011030" },
        { "G80026360" },
        { "H55155650" },
        { "J20831630" },
        { "N0071290B" },
        { "P0612600B" },
        { "Q2801276B" },
        { "R2800681B" },
        { "S7800001B" },
        { "U16808790" },
        { "V41146310" },
        { "W0041692B" },
        { "Q5000391Z" },    // All of the following CIF have a wrong initial letter.
        { "X5000391G" },
        { "K4599640T" },    // All of the following CIF have an old format and are no longer used.
        { "L7995811T" },
        { "M6780094T" },
        { "42628883Q" },    // This is a valid NIF.
        { "Z5490130N" }     // This is a valid NIE.
    };

    public static readonly TheoryData<string?> s_validNieData = new()
    {
        { "X2365053D" },    // All of the following NIE have been generated with the help of computer software,
        { "Z6819311T" },    // and any coincidence with a real natural person's NIE is purely coincidental.
        { "Y3329172M" },
        { "X9286006D" },
        { "Z2179773E" },
        { "Y6729141E" },
        { "X6227792J" },
        { "Z0785456B" },
        { "Y7903201R" }
    };

    public static readonly TheoryData<string?> s_wrongNieData = new()
    {
        { null },           // This NIE is null.
        { "" },             // This NIE is empty.
        { "   "},           // This NIE only has blank spaces.
        { "X32809633J" },   // All of the following NIE have an extra invalid length.
        { "Z73084144P" },
        { "Y31301744A" },
        { "Y262918H" },     // All of the following NIE have a short length.
        { "X389354Z" },
        { "Z157559B" },
        { "Y6698899Z" },    // All of the following NIE have a wrong letter.
        { "Z0204753M" },
        { "X2720374B" },
        { "YABCDEFGH" },    // All of the following NIE are non-numeric.
        { "ZABCDEFGH" },
        { "XABCDEFGH" },
        { "V41146317" },    // This is a valid CIF.
        { "48005439T" }     // This is a valid NIF.
    };

    public static readonly TheoryData<string?> s_validNifData = new()
    {
        { "49000444W" },    // All of the following NIF have been generated with the help of computer software,
        { "00384643Z" },    // and any coincidence with a real natural person's NIF is purely coincidental.
        { "50572394V" },
        { "34941726B" },
        { "07107030F" },
        { "17859381L" },
        { "44568536X" },
        { "62760060Y" },
        { "87612719S" }
    };

    public static readonly TheoryData<string?> s_wrongNifData = new()
    {
        { null },           // This NIF is null.
        { "" },             // This NIF is empty.
        { "   "},           // This NIF only has blank spaces.
        { "640945890Y" },   // All of the following NIF have an extra invalid length.
        { "813019590H" },
        { "380500330Y" },
        { "4342780X" },     // All of the following NIF have a short length.
        { "4970506L" },
        { "5077737W" },
        { "30714680A" },    // All of the following NIF have a wrong letter.
        { "82136085B" },
        { "96122027S" },
        { "ABCDEFGHI" },    // All of the following NIF are non-numeric.
        { "JKLMNÑOPQ" },
        { "RSTUVWXYZ" },
        { "B16713117" },    // This is a valid CIF.
        { "X1127287B" }     // This is a valid NIE.
    };

    [Theory]
    [MemberData(nameof(s_wrongTaxIdentificationNumberData))]
    public Task IsValid_should_returns_false_when_tax_identification_number_is_wrong(string? value, NifPerson type)
    {
        Assert.False(NifValidator.IsValid(value, type));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_validTaxIdentificationNumberData))]
    public Task IsValid_should_returns_true_when_tax_identification_number_is_valid(string? value, NifPerson type)
    {
        Assert.True(NifValidator.IsValid(value, type));
        return Task.CompletedTask;
    }

    [Theory]
    [InlineData("ABCDEFGHI")]
    [InlineData("U10418440")]
    [InlineData("Y7470363T")]
    public Task IsValid_should_always_returns_false_for_wrong_tax_identification_number_type(string? value)
    {
        Assert.False(NifValidator.IsValid(value, NifPerson.Wrong));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_wrongCifData))]
    public Task IsValidCif_should_returns_false_when_cif_is_wrong(string? sut)
    {
        Assert.False(NifValidator.IsValidCif(sut));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_validCifData))]
    public Task IsValidCif_should_returns_true_when_cif_is_valid(string? sut)
    {
        Assert.True(NifValidator.IsValidCif(sut));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_validNieData))]
    public Task IsValidNie_should_returns_true_when_nie_is_valid(string? sut)
    {
        Assert.True(NifValidator.IsValidNie(sut));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_wrongNieData))]
    public Task IsValidNie_should_returns_false_when_nie_is_wrong(string? sut)
    {
        Assert.False(NifValidator.IsValidNie(sut));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_validNifData))]
    public Task IsValidNif_should_returns_true_when_nif_is_valid(string? sut)
    {
        Assert.True(NifValidator.IsValidNif(sut));
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_wrongNifData))]
    public Task IsValidNif_should_returns_false_when_nif_is_wrong(string? sut)
    {
        Assert.False(NifValidator.IsValidNif(sut));
        return Task.CompletedTask;
    }
}
