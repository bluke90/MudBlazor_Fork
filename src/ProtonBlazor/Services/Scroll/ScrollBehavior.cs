// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace ProtonBlazor;

/// <summary>
/// Specifies the scroll behavior for scrolling operations.
/// </summary>
[EnumExtensions]
public enum ScrollBehavior
{
    /// <summary>
    /// Scrolls in a smooth fashion.
    /// </summary>
    [Description("smooth")]
    Smooth,

    /// <summary>
    /// Scrolls immediately.
    /// </summary>
    [Description("auto")]
    Auto
}
