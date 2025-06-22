// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemTimeAndLanguageTests.cs" company="OscarNET-SOFTware">
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

using System.Runtime.InteropServices;

using Microsoft.Extensions.Time.Testing;

namespace Mondongo.Iberico.Foundation.Tests;

public sealed class SystemTimeAndLanguageTests
{
    // America/Mexico_City   :   SDT(-06:00) | DST(-06:00) | SummerTime(2025-06-20 / 2025-09-22)
    // America/New_York      :   SDT(-05:00) | DST(-04:00) | SummerTime(2025-06-20 / 2025-09-22)
    // Atlantic/Canary       :   SDT(+00:00) | DST(+01:00) | SummerTime(2025-06-21 / 2026-09-22)
    // Australia/Canberra    :   SDT(+10:00) | DST(+11:00) | SummerTime(2025-12-01 / 2026-02-28)
    // Europe/London         :   SDT(+00:00) | DST(+01:00) | SummerTime(2025-06-21 / 2026-09-22)
    // Europe/Madrid         :   SDT(+01:00) | DST(+02:00) | SummerTime(2025-06-21 / 2026-09-22)

    private static readonly DateTime s_utcTime = new(2025, 6, 22, 14, 30, 0, DateTimeKind.Utc);

    private static readonly TimeZoneInfo s_timeZoneAmericaMexicoCity = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Central Standard Time (Mexico)" : "America/Mexico_City");

    private static readonly TimeZoneInfo s_timeZoneAmericaNewYork = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Eastern Standard Time" : "America/New_York");

    private static readonly TimeZoneInfo s_timeZoneAtlanticCanary = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "GMT Standard Time" : "Atlantic/Canary");

    private static readonly TimeZoneInfo s_timeZoneAustraliaCanberra = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "AUS Eastern Standard Time" : "Australia/Canberra");

    private static readonly TimeZoneInfo s_timeZoneEuropeLondon = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "GMT Standard Time" : "Europe/London");

    private static readonly TimeZoneInfo s_timeZoneEuropeMadrid = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Romance Standard Time" : "Europe/Madrid");

    public static readonly TheoryData<string, DateTime, string> s_timeAndLanguageData = new()
    {
        { s_timeZoneAmericaMexicoCity.Id, TimeZoneInfo.ConvertTimeFromUtc(s_utcTime, s_timeZoneAmericaMexicoCity), "es-MX" },
        { s_timeZoneAmericaNewYork.Id , TimeZoneInfo.ConvertTimeFromUtc(s_utcTime, s_timeZoneAmericaNewYork) , "en-US" },
        { s_timeZoneAtlanticCanary.Id , TimeZoneInfo.ConvertTimeFromUtc(s_utcTime, s_timeZoneAtlanticCanary) , "es-ES" },
        { s_timeZoneAustraliaCanberra.Id , TimeZoneInfo.ConvertTimeFromUtc(s_utcTime, s_timeZoneAustraliaCanberra) , "en-AU" },
        { s_timeZoneEuropeLondon.Id , TimeZoneInfo.ConvertTimeFromUtc(s_utcTime, s_timeZoneEuropeLondon) , "en-GB" },
        { s_timeZoneEuropeMadrid.Id , TimeZoneInfo.ConvertTimeFromUtc(s_utcTime, s_timeZoneEuropeMadrid) , "es-ES" }
    };

    [Fact]
    public Task Should_returns_expected_local_now()
    {
        SystemTimeProviderInternal timeProviderMadrid = new(s_timeZoneEuropeMadrid.Id);
        SystemTimeProviderInternal timeProviderMexico = new(s_timeZoneAmericaMexicoCity.Id);

        var sutMadrid = new SystemTimeAndLanguage();
        var sutMexico = new SystemTimeAndLanguage("es-MX", s_timeZoneAmericaMexicoCity.Id);

        Assert.Equal(expected: timeProviderMadrid.GetLocalNow().DateTime,
                     actual: sutMadrid.GetLocalNow(),
                     precision: TimeSpan.FromSeconds(1));

        Assert.Equal(expected: timeProviderMexico.GetLocalNow().DateTime,
                     actual: sutMexico.GetLocalNow(),
                     precision: TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    [Fact]
    public Task Should_returns_expected_utc_now()
    {
        SystemTimeProviderInternal timeProviderMadrid = new(s_timeZoneEuropeMadrid.Id);
        SystemTimeProviderInternal timeProviderMexico = new(s_timeZoneAmericaMexicoCity.Id);

        var sutMadrid = new SystemTimeAndLanguage();
        var sutMexico = new SystemTimeAndLanguage("es-MX", s_timeZoneAmericaMexicoCity.Id);

        Assert.Equal(expected: TimeZoneInfo.ConvertTimeToUtc(timeProviderMadrid.GetLocalNow().DateTime, s_timeZoneEuropeMadrid),
                     actual: sutMadrid.GetUtcNow(),
                     precision: TimeSpan.FromSeconds(1));

        Assert.Equal(expected: TimeZoneInfo.ConvertTimeToUtc(timeProviderMexico.GetLocalNow().DateTime, s_timeZoneAmericaMexicoCity),
                     actual: sutMexico.GetUtcNow(),
                     precision: TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_timeAndLanguageData))]
    public Task Should_returns_local_now_according_specified_time_provider(string timeZoneId,
                                                                           DateTime localTime,
                                                                           string cultureName)
    {
        FakeTimeProvider fakeTimeProvider = new();
        fakeTimeProvider.SetLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        fakeTimeProvider.SetUtcNow(s_utcTime);

        var sut = new SystemTimeAndLanguage(cultureName, fakeTimeProvider);

        Assert.Equal(expected: localTime,
                     actual: sut.GetLocalNow(),
                     precision: TimeSpan.Zero);

        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_timeAndLanguageData))]
    public Task Should_returns_utc_now_according_specified_time_provider(string timeZoneId,
                                                                         DateTime localTime,
                                                                         string cultureName)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        FakeTimeProvider fakeTimeProvider = new();
        fakeTimeProvider.SetLocalTimeZone(timeZone);
        fakeTimeProvider.SetUtcNow(s_utcTime);

        var sut = new SystemTimeAndLanguage(cultureName, fakeTimeProvider);

        Assert.Equal(expected: TimeZoneInfo.ConvertTimeToUtc(localTime, timeZone),
                     actual: sut.GetUtcNow(),
                     precision: TimeSpan.Zero);

        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(s_timeAndLanguageData))]
    public Task Should_returns_same_culture_name_and_time_zone_identifier(string timeZoneId,
                                                                          DateTime _,
                                                                          string cultureName)
    {
        var sut = new SystemTimeAndLanguage(cultureName, timeZoneId);

        Assert.Equal(expected: cultureName, actual: sut.Culture.Name, ignoreCase: true);
        Assert.Equal(expected: timeZoneId, actual: sut.TimeZone.Id, ignoreCase: true);

        return Task.CompletedTask;
    }

    [Fact]
    public Task Should_returns_same_culture_name_and_time_zone_identifier_when_using_default_constructor()
    {
        string cultureName = "es-ES";
        string timeZoneId = s_timeZoneEuropeMadrid.Id;

        var sut = new SystemTimeAndLanguage(cultureName, timeZoneId);

        Assert.Equal(expected: cultureName, actual: sut.Culture.Name, ignoreCase: true);
        Assert.Equal(expected: timeZoneId, actual: sut.TimeZone.Id, ignoreCase: true);

        return Task.CompletedTask;
    }
}
