using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// A set of methods which generate CSS classes for <see cref="ProBaseInput{T}" /> components.
/// </summary>
internal static class ProInputCssHelper
{
    /// <summary>
    /// Gets the CSS classes for the specified input component.
    /// </summary>
    /// <typeparam name="T">The type of data collect by the input.</typeparam>
    /// <param name="baseInput">The input control to use.</param>
    /// <param name="shrinkWhen">The function which determines when to shrink the input.</param>
    /// <returns>A set of CSS classes.</returns>
    public static string GetClassname<T>(ProBaseInput<T> baseInput, Func<bool> shrinkWhen) =>
        new CssBuilder("pro-input")
            .AddClass($"pro-input-{baseInput.Variant.ToStringFast(true)}")
            .AddClass($"pro-input-{baseInput.Variant.ToStringFast(true)}-with-label", !string.IsNullOrEmpty(baseInput.Label))
            .AddClass($"pro-input-adorned-{baseInput.Adornment.ToStringFast(true)}", baseInput.Adornment != Adornment.None)
            .AddClass($"pro-input-margin-{baseInput.Margin.ToStringFast(true)}", () => baseInput.Margin != Margin.None)
            .AddClass("pro-input-underline", () => baseInput.Underline && baseInput.Variant != Variant.Outlined)
            .AddClass("pro-shrink", shrinkWhen)
            .AddClass("pro-disabled", baseInput.Disabled)
            .AddClass("pro-input-error", baseInput.HasErrors)
            .AddClass("pro-ltr", baseInput.GetInputType() == InputType.Email || baseInput.GetInputType() == InputType.Telephone)
            .AddClass($"pro-typography-{baseInput.Typo.ToStringFast(true)}")
            .AddClass(baseInput.Class)
            .Build();

    /// <summary>
    /// Gets the CSS classes for the specified input component slot.
    /// </summary>
    /// <typeparam name="T">The type of data collect by the input.</typeparam>
    /// <param name="baseInput">The input control to use.</param>
    /// <returns>A set of CSS classes.</returns>
    public static string GetInputClassname<T>(ProBaseInput<T> baseInput) =>
        new CssBuilder("pro-input-slot")
            .AddClass("pro-input-root")
            .AddClass($"pro-input-root-{baseInput.Variant.ToStringFast(true)}")
            .AddClass($"pro-input-root-adorned-{baseInput.Adornment.ToStringFast(true)}", baseInput.Adornment != Adornment.None)
            .AddClass($"pro-input-root-margin-{baseInput.Margin.ToStringFast(true)}", () => baseInput.Margin != Margin.None)
            .AddClass(baseInput.Class)
            .Build();

    /// <summary>
    /// Gets the CSS classes for the specified input adornment.
    /// </summary>
    /// <typeparam name="T">The type of data collect by the input.</typeparam>
    /// <param name="baseInput">The input control to use.</param>
    /// <returns>A set of CSS classes.</returns>
    public static string GetAdornmentClassname<T>(ProBaseInput<T> baseInput) =>
        new CssBuilder()
            .AddClass($"pro-input-adornment-{baseInput.Adornment.ToStringFast(true)}", baseInput.Adornment != Adornment.None)
            .AddClass($"pro-text", !string.IsNullOrEmpty(baseInput.AdornmentText))
            .AddClass($"pro-input-root-filled-shrink", baseInput.Variant == Variant.Filled)
            .AddClass(baseInput.Class)
            .Build();
}
