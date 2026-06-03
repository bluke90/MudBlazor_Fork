// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Defines how the data grid edit form behaves after an edit operation.
/// </summary>
public enum DataGridEditFormAction
{
    /// <summary>
    /// Close the edit form after <see cref="ProDataGrid{T}.CommittedItemChanges"/> has completed.
    /// </summary>
    Close,

    /// <summary>
    /// Keep the edit form open after <see cref="ProDataGrid{T}.CommittedItemChanges"/> has completed.
    /// </summary>
    KeepOpen
}
