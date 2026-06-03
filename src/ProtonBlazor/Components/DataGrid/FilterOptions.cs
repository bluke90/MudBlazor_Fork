// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;


/// <summary>
/// Represents additional options applied to the filter of a <see cref="ProDataGrid{T}"/>.
/// </summary>
public class FilterOptions
{
    /// <summary>
    /// The case sensitivity to apply for filters on <c>string</c> columns.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="DataGridFilterCaseSensitivity.Default"/>.
    /// </remarks>
    public DataGridFilterCaseSensitivity FilterCaseSensitivity { get; set; } = DataGridFilterCaseSensitivity.Default;

    /// <summary>
    /// The default options applied when no options are given.
    /// </summary>
    public static FilterOptions Default { get; } = new();
}
