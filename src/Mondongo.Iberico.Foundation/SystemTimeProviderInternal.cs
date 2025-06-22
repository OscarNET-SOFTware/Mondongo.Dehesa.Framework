// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemTimeProviderInternal.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Iberico.Foundation;

/// <summary>
/// Represents an internal system time provider.
/// </summary>
/// <param name="timeZoneId">The time zone identifier.</param>
internal sealed class SystemTimeProviderInternal(string timeZoneId) : TimeProvider
{
    /// <summary>
    /// Stores the inner time zone.
    /// </summary>
    private readonly TimeZoneInfo _timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

    /// <summary>
    /// Gets the local time zone according to this <see cref="SystemTimeProviderInternal" />'s notion of time.
    /// </summary>
    /// <value>A <see cref="TimeZoneInfo" /> object that represents the local time zone.</value>
    public override TimeZoneInfo LocalTimeZone => _timeZone;
}
