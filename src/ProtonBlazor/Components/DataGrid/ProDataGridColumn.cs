// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ProtonBlazor;

public sealed class ProDataGridColumn<TItem>
{
    public required string Title { get; init; }

    /// <summary>Extracts the display value when no <see cref="CellTemplate"/> is provided.</summary>
    public required Func<TItem, object?> ValueSelector { get; init; }

    /// <summary>The field name sent in <see cref="ServerDataRequest.SortField"/>. Null disables sorting for this column.</summary>
    public string? SortField { get; init; }

    /// <summary>Custom cell renderer. When set, <see cref="ValueSelector"/> is ignored for display.</summary>
    public RenderFragment<TItem>? CellTemplate { get; init; }

    /// <summary>CSS width value applied to the &lt;th&gt; element (e.g. "120px", "15%").</summary>
    public string? Width { get; init; }

    /// <summary>Extra CSS class applied to every &lt;td&gt; in this column.</summary>
    public string? CssClass { get; init; }

    public DataGridColumnAlign Align { get; init; } = DataGridColumnAlign.Left;
}
