// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemBootstrapper.cs" company="OscarNET-SOFTware">
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
/// Provides the system bootstrapper.
/// </summary>
public sealed class SystemBootstrapper
{
    /// <summary>
    /// Stores the lazy initialization for this instance.
    /// </summary>
    private static Lazy<SystemBootstrapper> s_instance = new(() => new(s_timeAndLanguage));

    /// <summary>
    /// Stores the system time and language settings.
    /// </summary>
    private static SystemTimeAndLanguage s_timeAndLanguage;

    /// <summary>
    /// Stores the value that indicates whether this instance has been initialized or not.
    /// </summary>
    private static bool s_initialized = false;

    /// <summary>
    /// Ensures the <see cref="SystemBootstrapper" /> class cannot be instantiated from outside.
    /// </summary>
    /// <param name="timeAndLanguage">The system time and language settings.</param>
    private SystemBootstrapper(SystemTimeAndLanguage timeAndLanguage)
    {
        TimeAndLanguage = timeAndLanguage.ApplyCulture();
    }

    /// <summary>
    /// Gets the unique instance of this class.
    /// </summary>
    /// <value>
    /// A <see cref="SystemBootstrapper" /> object that represents the unique instance of this class.
    /// </value>
    /// <exception cref="InvalidOperationException">
    /// The instance of <see cref="SystemBootstrapper" /> class has not yet been initialized.
    /// Please, call <see cref="Initialize(SystemTimeAndLanguage)" /> method before accessing it.
    /// </exception>
    public static SystemBootstrapper Instance
    {
        get
        {
            if (!s_initialized)
            {
                throw new InvalidOperationException(
                    $"Call {nameof(Initialize)}() before accessing {nameof(SystemBootstrapper)}.{nameof(Instance)}.");
            }

            return s_instance.Value;
        }
    }

    /// <summary>
    /// Gets the current system time and language settings.
    /// </summary>
    /// <value>A <see cref="SystemTimeAndLanguage" /> object that represents the current system time and language settings.</value>
    public SystemTimeAndLanguage TimeAndLanguage { get; }

    /// <summary>
    /// Provides initialization values to be used by the unique instance of <see cref="SystemBootstrapper" /> class.
    /// </summary>
    /// <param name="timeAndLanguage">The system time and language settings.</param>
    /// <exception cref="InvalidOperationException">
    /// The instance of <see cref="SystemBootstrapper" /> class has already been initialized.
    /// </exception>
    public static void Initialize(SystemTimeAndLanguage timeAndLanguage)
    {
        if (s_initialized)
        {
            throw new InvalidOperationException($"{nameof(SystemBootstrapper)} has already been initialized.");
        }

        s_timeAndLanguage = timeAndLanguage;
        s_initialized = true;
    }

    /// <summary>
    /// Resets this singleton for testing purposes.
    /// </summary>
    internal static void ResetForTesting()
    {
        s_instance = new(() => new(s_timeAndLanguage));
        s_initialized = false;
    }
}
