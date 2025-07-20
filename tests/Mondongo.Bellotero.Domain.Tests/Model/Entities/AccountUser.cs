// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountUser.cs" company="OscarNET-SOFTware">
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

using System.Security;

namespace Mondongo.Bellotero.Domain.Model.Entities;

public class AccountUser : EntityBase<AccountUserId>
{
    public AccountUser(AccountUserId id, string accountName, SecureString accountPassword)
        : base(id)
    {
        AccountName = accountName;
        AccountPassword = accountPassword;
    }

    protected AccountUser()
    : base()
    {
        // Parameterless constructor required by ORM.
    }

    public string AccountName { get; protected set; } = null!;
    public SecureString AccountPassword { get; protected set; } = null!;
}
