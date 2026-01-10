# Column Definitions

Column definitions let you define DataGrid columns as data in a view model. You bind `ColumnDefinitionsSource` to a list of `DataGridColumnDefinition` and the grid materializes the built-in column types for you. This keeps view models free of Avalonia control instances and keeps columns fully typed and in sync.

## When to use column definitions

- You want MVVM-friendly columns without constructing `DataGridColumn` controls in view models.
- You want to reuse column metadata across multiple views.
- You need string-keyed templates/themes and AOT-friendly compiled bindings.

## Basic setup

```xml
<DataGrid ItemsSource="{Binding Items}"
          ColumnDefinitionsSource="{Binding ColumnDefinitions}"
          AutoGenerateColumns="False"
          HeadersVisibility="All">
  <DataGrid.Resources>
    <DataTemplate x:Key="StatusBadgeTemplate">
      <Border Padding="6,2" CornerRadius="8">
        <TextBlock Text="{Binding Status}" />
      </Border>
    </DataTemplate>
  </DataGrid.Resources>
</DataGrid>
```

```csharp
public ObservableCollection<DataGridColumnDefinition> ColumnDefinitions { get; } = new()
{
    new DataGridTextColumnDefinition
    {
        Header = "First Name",
        Binding = DataGridBindingDefinition.Create<Person, string>(p => p.FirstName),
        Width = new DataGridLength(1.2, DataGridLengthUnitType.Star)
    },
    new DataGridTextColumnDefinition
    {
        Header = "Last Name",
        Binding = DataGridBindingDefinition.Create<Person, string>(p => p.LastName),
        Width = new DataGridLength(1.2, DataGridLengthUnitType.Star)
    },
    new DataGridTemplateColumnDefinition
    {
        Header = "Badge",
        CellTemplateKey = "StatusBadgeTemplate",
        IsReadOnly = true
    }
};
```

## Definition types and property mapping

Each built-in column has a matching `*ColumnDefinition` type (for example, `DataGridTextColumnDefinition`). Definition properties mirror the column properties.

- `Header`, `Width`, `MinWidth`, `MaxWidth`, `DisplayIndex`, `IsVisible`, `CanUserSort`, `IsReadOnly`, and more.
- Template/theme keys: `HeaderTemplateKey`, `CellTemplateKey`, `CellEditingTemplateKey`, `HeaderThemeKey`, `CellThemeKey`, `FilterThemeKey`.
- Style hooks: `CellStyleClasses`, `HeaderStyleClasses`.
- Bindings: `Binding`, `SelectedItemBinding`, `SelectedValueBinding`, `TextBinding`, and other `DataGridBindingDefinition`-backed properties.

For the full catalog, see [Column Types Reference](column-types-reference.md).

## Template and theme keys

Template and theme properties use string keys and are resolved against grid resources first, then application resources:

- `HeaderTemplateKey`, `CellTemplateKey`, `CellEditingTemplateKey`, `NewRowCellTemplateKey`
- `HeaderThemeKey`, `CellThemeKey`, `FilterThemeKey`

Keep the templates in `DataGrid.Resources` or `Application.Resources` to decouple view models from visuals.

## Updates, lifetime, and threading

- `ColumnDefinitionsSource` tracks `INotifyCollectionChanged` and applies add/remove/reset changes.
- Each definition is `INotifyPropertyChanged`; updates are pushed to the materialized column.
- Changes must happen on the UI thread and the same thread that created the binding.
- `ColumnDefinitionsSource` cannot be used together with bound `Columns` or inline column declarations.

## Bound columns and value accessors

`DataGridBindingDefinition` creates a compiled binding and also provides a typed value accessor used by sorting, filtering, searching, and conditional formatting. For computed values or non-binding scenarios, set `ValueAccessor` (and optionally `ValueType`) on the definition.

```csharp
new DataGridTextColumnDefinition
{
    Header = "Total",
    ValueAccessor = new DataGridColumnValueAccessor<Order, decimal>(o => o.Price * o.Quantity),
    ValueType = typeof(decimal),
    IsReadOnly = true
};
```

## AOT and fast path

Expression-based binding creation requires dynamic code generation. For AOT-friendly bindings and zero-reflection paths, use the overloads that accept a prebuilt `CompiledBindingPath` or `IPropertyInfo`. See:

- [Column Definitions: AOT-Friendly Bindings](column-definitions-aot.md)
- [Column Definitions: Model Integration and Fast Path](column-definitions-models.md)
- [Column Definitions: Hot Path Integration](column-definitions-hot-path.md)
- [Column Definitions: Hierarchical Columns](column-definitions-hierarchical.md)
