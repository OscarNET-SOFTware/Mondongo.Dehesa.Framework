// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemTimeLanguage.cs" company="OscarNET-SOFTware">
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

using System.Globalization;
using System.Runtime.InteropServices;

namespace Mondongo.Iberico.Foundation;

/// <summary>
/// Represents a system time and language settings.
/// </summary>
public readonly struct SystemTimeAndLanguage
{
    /// <summary>
    /// Stores the inner time provider.
    /// </summary>
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemTimeAndLanguage" /> structure.
    /// </summary>
    public SystemTimeAndLanguage()
        : this(cultureName: "es-ES",
               timeZoneId: RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                                ? "Romance Standard Time"
                                : "Europe/Madrid")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemTimeAndLanguage" /> structure.
    /// </summary>
    /// <param name="cultureName">The culture name.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    public SystemTimeAndLanguage(string cultureName, string timeZoneId)
        : this(cultureName: cultureName,
               timeProvider: new SystemTimeProviderInternal(timeZoneId))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemTimeAndLanguage" /> structure (for testing purposes).
    /// </summary>
    /// <param name="cultureName">The culture name.</param>
    /// <param name="timeProvider">The time provider.</param>
    internal SystemTimeAndLanguage(string cultureName, TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        Culture = CultureInfo.ReadOnly(new CultureInfo(cultureName, useUserOverride: false));
        TimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeProvider.LocalTimeZone.Id);
    }

    /// <summary>
    /// Gets the culture.
    /// </summary>
    /// <value>A <see cref="CultureInfo" /> object that represents the culture.</value>
    public CultureInfo Culture { get; init; }

    /// <summary>
    /// Gets the time zone.
    /// </summary>
    /// <value>A <see cref="TimeZoneInfo" /> object that represents the time zone.</value>
    public TimeZoneInfo TimeZone { get; init; }

    /// <summary>
    /// Gets the current local date and time according to the time zone used by this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="DateTime" /> object whose value is the current local date and time
    /// according to the time zone specified in <see cref="TimeZone"/> property.
    /// </returns>
    public readonly DateTime GetLocalNow() =>
        TimeZoneInfo.ConvertTimeFromUtc(_timeProvider.GetUtcNow().DateTime, TimeZone);

    /// <summary>
    /// Gets the current local date and time according to the time zone used by this instance,
    /// expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    /// <returns>
    /// A <see cref="DateTime" /> object whose value is the current local date and time
    /// according to the time zone specified in <see cref="TimeZone"/> property, expressed as
    /// the Coordinated Universal Time (UTC).
    /// </returns>
    public readonly DateTime GetUtcNow() => _timeProvider.GetUtcNow().DateTime;

    /// <summary>
    /// Applies the culture of this instance to all new threads and also to the current thread.
    /// </summary>
    /// <returns>
    /// A <see cref="SystemTimeAndLanguage"/> object thats represents this instance.
    /// </returns>
    internal readonly SystemTimeAndLanguage ApplyCulture()
    {
        CultureInfo.DefaultThreadCurrentCulture = Culture;
        CultureInfo.DefaultThreadCurrentUICulture = Culture;
        Thread.CurrentThread.CurrentCulture = Culture;
        Thread.CurrentThread.CurrentUICulture = Culture;

        return this;
    }
}
