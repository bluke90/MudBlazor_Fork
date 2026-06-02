// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace ProtonBlazor;

/// <summary>
/// Indicates the sorting mode for a <see cref="ProDataGrid{T}"/>.
/// </summary>
[EnumExtensions]
public enum SortMode
{
    /// <summary>
    /// Sorting is not allowed.
    /// </summary>
    [Description("none")]
    None,

    /// <summary>
    /// Sorting can occur for one column at a time.
    /// </summary>
    [Description("single")]
    Single,

    /// <summary>
    /// Sorting can be done for multiple columns.
    /// </summary>
    [Description("multiple")]
    Multiple
}
