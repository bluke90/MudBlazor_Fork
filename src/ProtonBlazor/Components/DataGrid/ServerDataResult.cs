// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class ServerDataResult<TItem>
{
    public required IReadOnlyList<TItem> Items { get; init; }
    /// <summary>Total matching rows across all pages (used for pagination display).</summary>
    public required int TotalCount { get; init; }
}
