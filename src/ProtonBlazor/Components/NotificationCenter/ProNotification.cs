// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class ProNotification
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public string? Message { get; init; }
    public NotificationSeverity Severity { get; init; } = NotificationSeverity.Info;
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
    public bool IsRead { get; set; }
    public string? Icon { get; init; }
    public Func<Task>? Action { get; init; }
}
