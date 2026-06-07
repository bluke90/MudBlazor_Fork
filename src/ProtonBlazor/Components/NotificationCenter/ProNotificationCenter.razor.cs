// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

public partial class ProNotificationCenter : ProComponentBase, IDisposable
{
    [Inject] private IProNotificationService NotificationService { get; set; } = null!;

    /// <summary>Which edge of the bell button the panel opens from.</summary>
    [Parameter] public NotificationPanelAlign Align { get; set; } = NotificationPanelAlign.Right;

    /// <summary>Maximum number of notifications shown in the panel (0 = unlimited).</summary>
    [Parameter] public int MaxItems { get; set; } = 50;

    private bool _isOpen;

    private string _wrapperClass => new CssBuilder("pro-nc")
        .AddClass("pro-nc--align-left", Align == NotificationPanelAlign.Left)
        .AddClass(Class)
        .Build();

    protected override void OnInitialized() =>
        NotificationService.StateChanged += HandleStateChanged;

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    private void TogglePanel() => _isOpen = !_isOpen;
    private void ClosePanel() => _isOpen = false;

    private async Task HandleItemClick(ProNotification notification)
    {
        NotificationService.MarkRead(notification.Id);
        if (notification.Action is not null)
            await notification.Action();
    }

    private void MarkAllRead() => NotificationService.MarkAllRead();

    private void ClearAll()
    {
        NotificationService.Clear();
        _isOpen = false;
    }

    private static string GetItemClass(ProNotification n) =>
        new CssBuilder("pro-nc-item")
            .AddClass("pro-nc-item--unread", !n.IsRead)
            .Build();

    private static string FormatTime(DateTimeOffset ts)
    {
        var elapsed = DateTimeOffset.UtcNow - ts;
        if (elapsed.TotalSeconds < 60) return "just now";
        if (elapsed.TotalMinutes < 60) return $"{(int)elapsed.TotalMinutes}m ago";
        if (elapsed.TotalHours < 24) return $"{(int)elapsed.TotalHours}h ago";
        if (elapsed.TotalDays < 2) return "yesterday";
        if (elapsed.TotalDays < 7) return $"{(int)elapsed.TotalDays}d ago";
        return ts.LocalDateTime.ToString("MMM d");
    }

    public void Dispose() =>
        NotificationService.StateChanged -= HandleStateChanged;
}
