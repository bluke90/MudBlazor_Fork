// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;


/// <summary>
/// Indicates the behavior which begins inline editing for a <see cref="ProTable{T}"/>.
/// </summary>
public enum TableEditTrigger
{
    /// <summary>
    /// Editing begins when a row is clicked.
    /// </summary>
    RowClick,

    /// <summary>
    /// Editing begins when the edit button is clicked.
    /// </summary>
    EditButton,
}
