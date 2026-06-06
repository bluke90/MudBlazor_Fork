// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ProtonBlazor;

public partial class ProStreamingText : ProComponentBase
{
    /// <summary>The text content to display. Append new tokens to grow the stream.</summary>
    [Parameter] public string? Content { get; set; }

    /// <summary>When true a blinking cursor renders after the content.</summary>
    [Parameter] public bool IsStreaming { get; set; }

    private string _class => new CssBuilder("pro-streaming-text")
        .AddClass("pro-streaming-text--streaming", IsStreaming)
        .AddClass(Class)
        .Build();
}
