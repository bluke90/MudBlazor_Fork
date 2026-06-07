// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ProtonBlazor;

public partial class ProFormSchema : ProComponentBase
{
    [Parameter, EditorRequired] public IReadOnlyList<FieldDefinition> Schema { get; set; } = [];
    [Parameter] public IReadOnlyDictionary<string, object?>? InitialValues { get; set; }
    [Parameter] public EventCallback<Dictionary<string, object?>> OnSubmit { get; set; }
    [Parameter] public EventCallback<(string Key, object? Value)> OnFieldChanged { get; set; }
    [Parameter] public string? SubmitLabel { get; set; }
    [Parameter] public bool ShowSubmitButton { get; set; } = true;
    [Parameter] public bool IsLoading { get; set; }

    private readonly Dictionary<string, object?> _values = new();
    private readonly Dictionary<string, string?> _errors = new();
    private bool _submitted;

    private string _formClass => new CssBuilder("pro-form-schema")
        .AddClass(Class)
        .Build();

    protected override void OnParametersSet()
    {
        foreach (var field in Schema.Where(f => !_values.ContainsKey(f.Key)))
        {
            _values[field.Key] = InitialValues is not null && InitialValues.TryGetValue(field.Key, out var iv)
                ? iv
                : field.DefaultValue;
        }
    }

    private string GetFieldId(string key) => $"pro-fs-{key}";

    private string? GetStringValue(string key) =>
        _values.TryGetValue(key, out var v) ? v?.ToString() : null;

    private bool GetBoolValue(string key) =>
        _values.TryGetValue(key, out var v) && v is bool b && b;

    private double? GetNumberValue(string key)
    {
        if (!_values.TryGetValue(key, out var v) || v is null) return null;
        if (v is double d) return d;
        if (v is int i) return i;
        return double.TryParse(v.ToString(), out var p) ? p : null;
    }

    private string? GetDateValue(string key)
    {
        if (!_values.TryGetValue(key, out var v) || v is null) return null;
        return v switch
        {
            DateTime dt => dt.ToString("yyyy-MM-dd"),
            DateTimeOffset dto => dto.Date.ToString("yyyy-MM-dd"),
            _ => v.ToString()
        };
    }

    private string? GetFieldError(string key) =>
        _submitted && _errors.TryGetValue(key, out var e) ? e : null;

    private string GetFieldWrapperClass(string key) =>
        new CssBuilder("pro-fs-field")
            .AddClass("pro-fs-field--error", GetFieldError(key) is not null)
            .Build();

    private static string GetInputType(FieldType type) => type switch
    {
        FieldType.Email => "email",
        FieldType.Password => "password",
        _ => "text"
    };

    private async Task OnFieldChange(FieldDefinition field, object? rawValue)
    {
        object? typed = field.Type switch
        {
            FieldType.Number or FieldType.Slider =>
                double.TryParse(rawValue?.ToString(), out var d) ? d : null,
            FieldType.Checkbox or FieldType.Switch =>
                rawValue is bool b ? b : bool.TryParse(rawValue?.ToString(), out var bv) && bv,
            FieldType.Date =>
                DateTime.TryParse(rawValue?.ToString(), out var dt) ? (object?)dt : null,
            _ => rawValue?.ToString()
        };

        _values[field.Key] = typed;

        if (_submitted)
            ValidateField(field);

        if (OnFieldChanged.HasDelegate)
            await OnFieldChanged.InvokeAsync((field.Key, typed));
    }

    private void ValidateField(FieldDefinition field)
    {
        var value = _values.TryGetValue(field.Key, out var v) ? v : null;

        string? error = null;

        if (field.Required)
        {
            error = value switch
            {
                null => $"{field.Label} is required.",
                string s when string.IsNullOrWhiteSpace(s) => $"{field.Label} is required.",
                _ => null
            };
        }

        if (error is null)
            error = field.Validate?.Invoke(value);

        _errors[field.Key] = error;
    }

    private bool ValidateAll()
    {
        foreach (var field in Schema)
            ValidateField(field);
        return _errors.Values.All(e => e is null);
    }

    private async Task HandleSubmit()
    {
        _submitted = true;
        if (!ValidateAll())
            return;

        if (OnSubmit.HasDelegate)
            await OnSubmit.InvokeAsync(new Dictionary<string, object?>(_values));
    }

    /// <summary>Programmatically trigger validation. Returns true if all fields are valid.</summary>
    public bool Validate()
    {
        _submitted = true;
        var valid = ValidateAll();
        StateHasChanged();
        return valid;
    }

    /// <summary>Returns a snapshot of the current field values.</summary>
    public Dictionary<string, object?> GetValues() => new(_values);

    /// <summary>Resets all values to defaults and clears validation state.</summary>
    public void Reset()
    {
        _submitted = false;
        _errors.Clear();
        _values.Clear();
        foreach (var field in Schema)
            _values[field.Key] = field.DefaultValue;
        StateHasChanged();
    }
}
