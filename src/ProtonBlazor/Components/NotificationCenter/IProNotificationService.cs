// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public interface IProNotificationService
{
    IReadOnlyList<ProNotification> Notifications { get; }
    int UnreadCount { get; }
    event Action? StateChanged;
    void Add(ProNotification notification);
    void Remove(string id);
    void MarkRead(string id);
    void MarkAllRead();
    void Clear();
}
