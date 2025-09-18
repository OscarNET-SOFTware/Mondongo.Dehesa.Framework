// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBaseTests.cs" company="OscarNET-SOFTware">
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

using System.Windows.Controls;

namespace Mondongo.Mangurrino.DesktopUI;

partial class EditFieldBaseTests
{
    private class TestEditFieldBase : EditFieldBase
    {
        private readonly Dictionary<string, object> _templateChildren = [];

        public void AddTemplateChild(string name, object element) => _templateChildren[name] = element;
        public EditFieldGridPosition InvokeGetGridPosition(EditFieldLabelPosition pos) => GetGridPosition(pos);
        public void InvokeOnApplyTemplate() => OnApplyTemplate();
        public void InvokeUpdateLabelPosition(EditFieldLabelPosition pos) => UpdateLabelPosition(this, pos);
        public void InvokeUpdateLabelMargin(EditFieldLabelPosition pos) => UpdateLabelMargin(this, pos);
        public void InvokeSetGridProperties(EditFieldGridPosition pos) => SetGridProperties(this, pos);

        public void SetupTemplateParts(Grid layoutRoot, Border labelContent, TextBlock label, Border fieldContent, TextBox field)
        {
            _layoutRoot = layoutRoot;
            _labelContent = labelContent;
            _label = label;
            _fieldContent = fieldContent;
            _field = field;
        }

        protected override object? GetTemplateChildWrapper(string childName)
        {
            if (_templateChildren.TryGetValue(childName, out object? element))
            {
                return element;
            }

            return null;
        }
    }

    private static (Grid, Border, TextBlock, Border, TextBox) OnApplyTemplateSimulation(TestEditFieldBase editField)
    {
        // Prepare simulated WPF elements
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        var labelContent = new Border();
        var label = new TextBlock();
        var fieldContent = new Border();
        var field = new TextBox();

        // Register elements for GetTemplateChildWrapper to return
        editField.AddTemplateChild("PART_LayoutRoot", grid);
        editField.AddTemplateChild("PART_LabelContent", labelContent);
        editField.AddTemplateChild("PART_Label", label);
        editField.AddTemplateChild("PART_FieldContent", fieldContent);
        editField.AddTemplateChild("PART_Field", field);

        // Call OnApplyTemplate that uses GetTemplateChildWrapper internally
        editField.InvokeOnApplyTemplate();

        return (grid, labelContent, label, fieldContent, field);
    }
}
