using Microsoft.AspNetCore.Components;
using ProtonBlazor.State;
using ProtonBlazor.Utilities;

namespace ProtonBlazor.UnitTests;

public partial class ParameterStateDependencyComp2 : ProComponentBase
{
    private readonly ParameterState<string?> _textState;
    private readonly ParameterState<ProColor?> _valueState;

    [Parameter, ParameterState]
    public string? Text { get; set; }

    [Parameter]
    public EventCallback<string?> TextChanged { get; set; }

    [Parameter, ParameterState]
    public ProColor? Value { get; set; }

    [Parameter]
    public EventCallback<ProColor?> ValueChanged { get; set; }

    public List<ParameterChangedEventArgs<string?>> TextChanges { get; } = [];

    public List<ParameterChangedEventArgs<ProColor?>> ValueChanges { get; } = [];

    public ParameterStateDependencyComp2()
    {
        using var registerScope = CreateRegisterScope();
        _textState = registerScope.RegisterParameter<string?>(nameof(Text))
            .WithParameter(() => Text)
            .WithEventCallback(() => TextChanged)
            .WithChangeHandler(OnTextChangedHandlerAsync);
        _valueState = registerScope.RegisterParameter<ProColor?>(nameof(Value))
            .WithParameter(() => Value)
            .WithEventCallback(() => ValueChanged)
            .WithChangeHandler(OnValuerChangedHandlerAsync);
    }

    private async Task OnTextChangedHandlerAsync(ParameterChangedEventArgs<string?> args)
    {
        TextChanges.Add(args);
        if (string.IsNullOrWhiteSpace(args.Value))
        {
            return;
        }

        if (args.ParameterView.TryGetValue<ProColor?>(nameof(Value), out var incomingValue))
        {
            if (incomingValue is null)
            {
                await _valueState.SetValueAsync(ProColor.Parse(args.Value));
                return;
            }

            var valueText = incomingValue.ToString(ProColorOutputFormats.Hex);

            // Conflict: both changed and they disagree
            if (!string.Equals(
                    args.Value,
                    valueText,
                    StringComparison.OrdinalIgnoreCase))
            {
                await _textState.SetValueAsync(valueText);
                return;
            }
        }

        await _valueState.SetValueAsync(ProColor.Parse(args.Value));
    }

    private Task OnValuerChangedHandlerAsync(ParameterChangedEventArgs<ProColor?> args)
    {
        ValueChanges.Add(args);
        return _textState.SetValueAsync(args.Value?.ToString(ProColorOutputFormats.Hex));
    }
}
