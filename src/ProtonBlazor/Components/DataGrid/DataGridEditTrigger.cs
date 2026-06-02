// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Indicates the behavior which begins editing a cell.
/// </summary>
public enum DataGridEditTrigger
{
    /// <summary>
    /// Editing begins when the row is clicked.
    /// </summary>
    OnRowClick,

    /// <summary>
    /// Editing begins when the edit button is clicked.
    /// </summary>
    Manual
}
