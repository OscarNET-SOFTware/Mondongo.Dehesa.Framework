// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Employee.cs" company="OscarNET-SOFTware">
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

using CSharpFunctionalExtensions;

namespace Mondongo.Bellotero.Domain.Model.Entities;

public class Employee : EntityBase
{
    internal const string IsMandatoryMessageForFullName = "The full name of the employee is mandatory!";

    public Employee(long id, string fullName, DateTime hireDate)
        : base(id)
    {
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        HireDate = hireDate;
    }

    protected Employee()
        : base()
    {
        // Parameterless constructor required by ORM.
    }

    public string FullName { get; protected set; } = null!;
    public DateTime HireDate { get; protected set; }

    public override Result Validate()
    {
        if (string.IsNullOrWhiteSpace(FullName))
        {
            return Result.Failure(IsMandatoryMessageForFullName);
        }

        return base.Validate();
    }
}
