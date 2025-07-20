// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountUserFactory.cs" company="OscarNET-SOFTware">
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

using System.Reflection;

using Mondongo.Iberico.Extensions;

namespace Mondongo.Bellotero.Domain.Model.Entities;

internal sealed class AccountUserFactory
{
    private const string Password = "MyPassword#123";

    public static AccountUser New(AccountUserId id)
        => new(id, $"{nameof(AccountUser)}_{Guid.NewGuid():N}", Password.ToSecureString());

    public static AccountUser Empty()  // ORM simulation :)
    => (AccountUser)typeof(AccountUser).GetConstructor(
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            types: Type.EmptyTypes,
            modifiers: null)
        !.Invoke(null);
}
