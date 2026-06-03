// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Indicates the type of aggregation to perform.
/// </summary>
public enum AggregateType
{
    /// <summary>
    /// Calculates the average of values.
    /// </summary>
    Avg,

    /// <summary>
    /// Calculates the number of values.
    /// </summary>
    Count,

    /// <summary>
    /// Calculates the aggregate using a custom function.
    /// </summary>
    Custom,

    /// <summary>
    /// Calculates the maximum value.
    /// </summary>
    Max,

    /// <summary>
    /// Calculates the minimum value.
    /// </summary>
    Min,

    /// <summary>
    /// Calculates the sum of values.
    /// </summary>
    Sum
}
