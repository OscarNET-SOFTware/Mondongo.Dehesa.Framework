// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeFactory.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Model.Entities;

internal sealed class EmployeeFactory
{
    public static Employee New(long id)
        => new(id, $"{nameof(Employee)}_{Guid.NewGuid():N}", DateTime.Today);

    public static Employee NewDeleted()
    {
        Employee deletedEmployee = New(id: DateTime.Now.Ticks);
        deletedEmployee.Delete();
        return deletedEmployee;
    }

    public static Employee Empty()  // ORM simulation :)
        => (Employee)typeof(Employee).GetConstructor(
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                types: Type.EmptyTypes,
                modifiers: null)
            !.Invoke(null);
}
