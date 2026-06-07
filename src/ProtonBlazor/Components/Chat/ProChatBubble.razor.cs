// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

public partial class ProChatBubble : ProComponentBase
{
    /// <summary>Whether this message was sent by the user or the assistant.</summary>
    [Parameter] public ChatBubbleOrigin Origin { get; set; } = ChatBubbleOrigin.Assistant;

    /// <summary>The message body. May include <see cref="ProStreamingText"/>.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// When true and <see cref="ChildContent"/> is null, renders a
    /// <see cref="ProThinkingIndicator"/> inside the bubble.
    /// </summary>
    [Parameter] public bool IsStreaming { get; set; }

    /// <summary>Icon name shown in the avatar circle (e.g. a MudBlazor icon path).</summary>
    [Parameter] public string? AvatarIcon { get; set; }

    /// <summary>
    /// One-or-two-letter initials shown when no <see cref="AvatarIcon"/> is provided.
    /// Defaults to "U" for User and "AI" for Assistant.
    /// </summary>
    [Parameter] public string? AvatarText { get; set; }

    /// <summary>Optional timestamp displayed below the bubble.</summary>
    [Parameter] public DateTimeOffset? Timestamp { get; set; }

    private string _initials => AvatarText ?? (Origin == ChatBubbleOrigin.User ? "U" : "AI");

    private string _rowClass => new CssBuilder("pro-chat-row")
        .AddClass("pro-chat-row--user", Origin == ChatBubbleOrigin.User)
        .AddClass("pro-chat-row--assistant", Origin == ChatBubbleOrigin.Assistant)
        .AddClass(Class)
        .Build();

    private string _bubbleClass => new CssBuilder("pro-chat-bubble")
        .AddClass("pro-chat-bubble--user", Origin == ChatBubbleOrigin.User)
        .AddClass("pro-chat-bubble--assistant", Origin == ChatBubbleOrigin.Assistant)
        .AddClass("pro-chat-bubble--streaming", IsStreaming)
        .Build();
}
