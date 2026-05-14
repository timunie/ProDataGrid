using System;

namespace Avalonia.Diagnostics;

/// <summary>
/// Receives committed property edits made by DevTools property editors.
/// </summary>
public interface IDevToolsPropertyEditHandler
{
    /// <summary>
    /// Called after a DevTools property editor commits a value to the inspected object.
    /// </summary>
    /// <param name="edit">The committed property edit.</param>
    void OnPropertyEdited(DevToolsPropertyEdit edit);
}

/// <summary>
/// Describes a committed DevTools property editor change.
/// </summary>
public sealed class DevToolsPropertyEdit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DevToolsPropertyEdit"/> class.
    /// </summary>
    /// <param name="inspectedObject">The object selected in the diagnostics tree.</param>
    /// <param name="target">The object that owns the edited property.</param>
    /// <param name="propertyName">The display property name.</param>
    /// <param name="xamlPropertyName">The property name suitable for XAML attribute mutation.</param>
    /// <param name="propertyType">The declared property type.</param>
    /// <param name="declaringType">The declaring type, when available.</param>
    /// <param name="oldValue">The value before the edit.</param>
    /// <param name="newValue">The value after the edit.</param>
    /// <param name="oldValueText">The invariant text representation before the edit, when available.</param>
    /// <param name="newValueText">The invariant text representation after the edit, when available.</param>
    /// <param name="isAttached">Whether the edited property is an attached Avalonia property.</param>
    /// <param name="isAvaloniaProperty">Whether the edited property is an Avalonia property.</param>
    public DevToolsPropertyEdit(
        AvaloniaObject inspectedObject,
        object target,
        string propertyName,
        string xamlPropertyName,
        Type propertyType,
        Type? declaringType,
        object? oldValue,
        object? newValue,
        string? oldValueText,
        string? newValueText,
        bool isAttached,
        bool isAvaloniaProperty)
    {
        InspectedObject = inspectedObject ?? throw new ArgumentNullException(nameof(inspectedObject));
        Target = target ?? throw new ArgumentNullException(nameof(target));
        PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        XamlPropertyName = xamlPropertyName ?? throw new ArgumentNullException(nameof(xamlPropertyName));
        PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        DeclaringType = declaringType;
        OldValue = oldValue;
        NewValue = newValue;
        OldValueText = oldValueText;
        NewValueText = newValueText;
        IsAttached = isAttached;
        IsAvaloniaProperty = isAvaloniaProperty;
    }

    /// <summary>
    /// Gets the object selected in the diagnostics tree.
    /// </summary>
    public AvaloniaObject InspectedObject { get; }

    /// <summary>
    /// Gets the object that owns the edited property.
    /// </summary>
    public object Target { get; }

    /// <summary>
    /// Gets the display property name.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Gets the property name suitable for XAML attribute mutation.
    /// </summary>
    public string XamlPropertyName { get; }

    /// <summary>
    /// Gets the declared property type.
    /// </summary>
    public Type PropertyType { get; }

    /// <summary>
    /// Gets the declaring type, when available.
    /// </summary>
    public Type? DeclaringType { get; }

    /// <summary>
    /// Gets the value before the edit.
    /// </summary>
    public object? OldValue { get; }

    /// <summary>
    /// Gets the value after the edit.
    /// </summary>
    public object? NewValue { get; }

    /// <summary>
    /// Gets the invariant text representation before the edit, when available.
    /// </summary>
    public string? OldValueText { get; }

    /// <summary>
    /// Gets the invariant text representation after the edit, when available.
    /// </summary>
    public string? NewValueText { get; }

    /// <summary>
    /// Gets a value indicating whether the edited property is an attached Avalonia property.
    /// </summary>
    public bool IsAttached { get; }

    /// <summary>
    /// Gets a value indicating whether the edited property is an Avalonia property.
    /// </summary>
    public bool IsAvaloniaProperty { get; }
}
