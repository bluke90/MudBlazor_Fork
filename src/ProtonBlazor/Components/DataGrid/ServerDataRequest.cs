// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class ServerDataRequest
{
    /// <summary>Zero-based page index.</summary>
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string? SortField { get; init; }
    public bool SortDescending { get; init; }
    /// <summary>Freetext filter string; null when the filter is empty.</summary>
    public string? Filter { get; init; }
}
