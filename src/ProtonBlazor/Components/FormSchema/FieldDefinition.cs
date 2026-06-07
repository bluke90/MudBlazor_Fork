// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class FieldDefinition
{
    public required string Key { get; init; }
    public required string Label { get; init; }
    public FormFieldType Type { get; init; } = FormFieldType.Text;
    public bool Required { get; init; }
    public bool Disabled { get; init; }
    public string? Placeholder { get; init; }
    public string? HelperText { get; init; }
    public object? DefaultValue { get; init; }
    public IReadOnlyList<SelectOption>? Options { get; init; }
    public double? Min { get; init; }
    public double? Max { get; init; }
    public double? Step { get; init; }
    public int? MaxLength { get; init; }
    public int? Rows { get; init; }
    public Func<object?, string?>? Validate { get; init; }
}
