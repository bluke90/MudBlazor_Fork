// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class ProNotificationService : IProNotificationService
{
    private readonly List<ProNotification> _notifications = [];

    public IReadOnlyList<ProNotification> Notifications => _notifications.AsReadOnly();
    public int UnreadCount => _notifications.Count(n => !n.IsRead);
    public event Action? StateChanged;

    public void Add(ProNotification notification)
    {
        _notifications.Insert(0, notification);
        StateChanged?.Invoke();
    }

    public void Remove(string id)
    {
        if (_notifications.RemoveAll(n => n.Id == id) > 0)
            StateChanged?.Invoke();
    }

    public void MarkRead(string id)
    {
        var n = _notifications.FirstOrDefault(n => n.Id == id);
        if (n is not null && !n.IsRead)
        {
            n.IsRead = true;
            StateChanged?.Invoke();
        }
    }

    public void MarkAllRead()
    {
        var changed = false;
        foreach (var n in _notifications.Where(n => !n.IsRead))
        {
            n.IsRead = true;
            changed = true;
        }
        if (changed) StateChanged?.Invoke();
    }

    public void Clear()
    {
        if (_notifications.Count == 0) return;
        _notifications.Clear();
        StateChanged?.Invoke();
    }
}
