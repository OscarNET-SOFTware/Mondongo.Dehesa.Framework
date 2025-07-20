// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicEntityTests.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Model;

public sealed class BasicEntityTests
{
    [Fact]
    public void Entity_underlying_identifier_is_the_same_as_its_identifier()
    {
        long sameId = 1L;
        Customer sut = CustomerFactory.New(sameId);

        Assert.Equal(sameId, ((IEntity)sut).Id);
        Assert.Equal(sameId, sut.Id);
    }

    [Fact]
    public void Entities_with_same_identifier_have_the_same_hash_code()
    {
        Employee sutA = EmployeeFactory.New(1L);
        Employee sutB = EmployeeFactory.New(1L);

        Assert.Equal(sutA.GetHashCode(), sutB.GetHashCode());
    }

    [Fact]
    public void Transient_entities_have_the_same_hash_code()
    {
        Employee sutA = EmployeeFactory.Empty();
        Employee sutB = EmployeeFactory.Empty();
        AccountUser sutC = AccountUserFactory.Empty();
        AccountUser sutD = AccountUserFactory.Empty();

        Assert.Equal(sutA.GetHashCode(), sutB.GetHashCode());
        Assert.Equal(sutC.GetHashCode(), sutD.GetHashCode());
    }

    [Fact]
    public void Proxy_entities_have_the_same_hash_code()
    {
        NHibernateEmployeeProxy sutNHibernateProxy = new();
        Employee sutNHibernateUnproxied = sutNHibernateProxy;

        Castle.Proxies.EFCoreEmployeeProxy sutEFCoreProxy = new();
        Employee sutEFCoreUnproxied = sutEFCoreProxy;

        Assert.Equal(sutNHibernateProxy.GetHashCode(), sutNHibernateUnproxied.GetHashCode());
        Assert.Equal(sutEFCoreProxy.GetHashCode(), sutEFCoreUnproxied.GetHashCode());
    }

    [Fact]
    public void Entity_is_transient_when_has_default_or_null_identifier()
    {
        Employee sutA = EmployeeFactory.Empty();
        AccountUser sutB = AccountUserFactory.Empty();

        Assert.Equal(default, sutA.Id);
        Assert.Null(sutB.Id);
        Assert.True(sutA.IsTransient);
        Assert.True(sutB.IsTransient);
    }
}
