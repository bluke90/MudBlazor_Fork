// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

public partial class ProThinkingIndicator : ProComponentBase
{
    /// <summary>Visual style of the indicator.</summary>
    [Parameter] public ThinkingVariant Variant { get; set; } = ThinkingVariant.Dots;

    /// <summary>Optional text label rendered beside the indicator (e.g. "Thinking…").</summary>
    [Parameter] public string? Label { get; set; }

    private string _class => new CssBuilder("pro-thinking")
        .AddClass($"pro-thinking--{Variant.ToString().ToLowerInvariant()}")
        .AddClass(Class)
        .Build();
}
