// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Indicates the filtering behavior for <see cref="ProDataGrid{T}"/> rows.
/// </summary>
public enum DataGridFilterMode
{
    /// <summary>
    /// All filters are managed in a single popover.
    /// </summary>
    Simple,

    /// <summary>
    /// Only the current column filter is managed in a popover.
    /// </summary>
    ColumnFilterMenu,

    /// <summary>
    /// Filters are shown as a row in the grid.
    /// </summary>
    ColumnFilterRow,
}
