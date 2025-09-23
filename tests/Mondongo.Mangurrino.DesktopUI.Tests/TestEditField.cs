// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEditField.cs" company="OscarNET-SOFTware">
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

internal class TestEditField : MemoField
{
    private readonly Dictionary<string, object> _templateChildren = [];

    public void AddTemplateChild(string name, object element) => _templateChildren[name] = element;
    public EditFieldGridPosition InvokeGetGridPosition(EditFieldLabelPosition labelPosition) => GetGridPosition(labelPosition);
    public void InvokeOnApplyTemplate() => OnApplyTemplate();
    public void InvokeUpdateLabelPosition(EditFieldLabelPosition labelPosition) => UpdateLabelPosition(this, labelPosition);
    public void InvokeUpdateLabelMargin(EditFieldLabelPosition labelPosition) => UpdateLabelMargin(this, labelPosition);
    public void InvokeSetGridProperties(EditFieldGridPosition labelPosition) => SetGridProperties(this, labelPosition);

    public TestEditFieldTemplateParts OnApplyTemplateSimulation()
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
        AddTemplateChild("PART_LayoutRoot", grid);
        AddTemplateChild("PART_LabelContent", labelContent);
        AddTemplateChild("PART_Label", label);
        AddTemplateChild("PART_FieldContent", fieldContent);
        AddTemplateChild("PART_Field", field);

        // Call OnApplyTemplate that uses GetTemplateChildWrapper internally
        InvokeOnApplyTemplate();

        return new TestEditFieldTemplateParts
        {
            PART_LayoutRoot = grid,
            PART_LabelContent = labelContent,
            PART_Label = label,
            PART_FieldContent = fieldContent,
            PART_Field = field
        };
    }

    public void SetupTemplateParts(TestEditFieldTemplateParts templateParts)
    {
        _layoutRoot = templateParts.PART_LayoutRoot;
        _labelContent = templateParts.PART_LabelContent;
        _label = templateParts.PART_Label;
        _fieldContent = templateParts.PART_FieldContent;
        _field = templateParts.PART_Field;
    }

    protected override object? GetTemplateChildWrapper(string childName)
    {
        if (_templateChildren.TryGetValue(childName, out object? element))
        {
            return element;
        }

        return null;
    }

    protected internal override bool TryParseFieldValue(string fieldText, out string? fieldValue)
    {
        fieldValue = fieldText;
        return true;
    }
}
