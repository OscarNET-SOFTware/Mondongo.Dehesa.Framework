// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainExtensions.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Extensions;

/// <summary>
/// Provides several extension methods to extend the domain functionality.
/// </summary>
public static class DomainExtensions
{
    /// <summary>
    /// Defines the proxy prefix of Entity Framework Core.
    /// </summary>
    private const string EFCoreProxyPrefix = "Castle.Proxies.";

    /// <summary>
    /// Defines the proxy postfix of NHibernate.
    /// </summary>
    private const string NHibernateProxyPostfix = "Proxy";

    /// <summary>
    /// Gets the unproxied type of a given object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// A <see cref="Type" /> object that represents the unproxied type of <paramref name="obj" />.
    /// </returns>
    public static Type GetUnproxiedType(this IEntity obj)
    {
        Type type = obj.GetType();
        string typeString = type.ToString();

        return (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
            ? type.BaseType!
            : type;
    }
}
