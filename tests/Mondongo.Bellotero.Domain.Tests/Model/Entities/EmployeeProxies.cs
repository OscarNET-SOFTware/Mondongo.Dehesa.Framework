// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeProxies.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Model.Entities;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Mondongo.Bellotero.Domain.Model
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public sealed class NHibernateEmployeeProxy : Employee
    {
    }
}

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Castle.Proxies
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public sealed class EFCoreEmployeeProxy : Employee
    {
    }
}
