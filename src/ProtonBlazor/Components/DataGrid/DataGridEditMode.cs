// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Indicates how values are edited for <see cref="ProDataGrid{T}"/> cells.
/// </summary>
public enum DataGridEditMode
{
    /// <summary>
    /// Values are edited in the cell.
    /// </summary>
    Cell,

    /// <summary>
    /// A dialog is shown to edit values.
    /// </summary>
    Form
}
