// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Represents the operation types for <see cref="IProPopoverHolder"/>.
/// </summary>
public enum PopoverHolderOperation
{
    /// <summary>
    /// Specifies the creation operation for a popover holder.
    /// </summary>
    Create = 0,

    /// <summary>
    /// Specifies the removal operation for a popover holder.
    /// </summary>
    Remove = 1,

    /// <summary>
    /// Specifies the update operation for a popover holder.
    /// </summary>
    Update = 2
}
