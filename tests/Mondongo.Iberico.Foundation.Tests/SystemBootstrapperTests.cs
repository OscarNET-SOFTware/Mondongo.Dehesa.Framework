// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemBootstrapperTests.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Iberico.Foundation.Tests;

public sealed class SystemBootstrapperTests
{
    private static readonly SystemTimeAndLanguage s_defaultTimeAndLanguage = new();

    public SystemBootstrapperTests()
    {
        SystemBootstrapper.ResetForTesting();
    }

    [Fact]
    public void Should_applies_current_culture_to_all_threads()
    {
        SystemBootstrapper.Initialize(s_defaultTimeAndLanguage);

        SystemTimeAndLanguage sut = SystemBootstrapper.Instance.TimeAndLanguage;

        Assert.Same(expected: CultureInfo.DefaultThreadCurrentCulture, actual: sut.Culture);
        Assert.Same(expected: CultureInfo.DefaultThreadCurrentUICulture, actual: sut.Culture);
        Assert.Same(expected: Thread.CurrentThread.CurrentCulture, actual: sut.Culture);
        Assert.Same(expected: Thread.CurrentThread.CurrentUICulture, actual: sut.Culture);
    }

    [Fact]
    public void Should_throws_expected_exception_when_is_not_initilized()
    {
        Assert.Throws<InvalidOperationException>(() => SystemBootstrapper.Instance);
    }

    [Fact]
    public void Should_throws_expected_exception_when_is_already_initilized()
    {
        SystemBootstrapper.Initialize(s_defaultTimeAndLanguage);

        Assert.Throws<InvalidOperationException>(() => SystemBootstrapper.Initialize(s_defaultTimeAndLanguage));
    }

    [Fact]
    public void Singleton_instance_is_same()
    {
        SystemBootstrapper.Initialize(s_defaultTimeAndLanguage);

        SystemBootstrapper sutA = SystemBootstrapper.Instance;
        SystemBootstrapper sutB = SystemBootstrapper.Instance;

        Assert.Same(sutA, sutB);
    }

    [Fact]
    public void Singleton_is_thread_safe()
    {
        SystemBootstrapper.Initialize(s_defaultTimeAndLanguage);

        SystemBootstrapper? sutA = null, sutB = null;
        var threadA = new Thread(() => sutA = SystemBootstrapper.Instance);
        var threadB = new Thread(() => sutB = SystemBootstrapper.Instance);
        threadA.Start();
        threadB.Start();
        threadA.Join();
        threadB.Join();

        Assert.Same(sutA, sutB);
    }
}
