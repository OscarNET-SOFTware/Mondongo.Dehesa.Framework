// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="INHibernateUnitOfWork.cs" company="OscarNET-SOFTware">
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

using NHibernate;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Extends the Unit of Work contract to support registration of custom NHibernate repositories per entity type.
/// </summary>
public interface INHibernateUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Registers a custom query repository implementation for the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <typeparam name="TRepository">Repository implementation type.</typeparam>
    /// <param name="factory">Factory function that receives the session
    /// and produces a query repository instance.</param>
    void RegisterQueryRepository<TAggregateRoot, TRepository>(Func<ISession, TRepository> factory)
        where TRepository : class, IQueryRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot;

    /// <summary>
    /// Registers a custom command repository implementation for the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <typeparam name="TRepository">Repository implementation type.</typeparam>
    /// <param name="factory">Factory function that receives the session
    /// and produces a command repository instance.</param>
    void RegisterCommandRepository<TAggregateRoot, TRepository>(Func<ISession, TRepository> factory)
        where TRepository : class, ICommandRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot;
}
