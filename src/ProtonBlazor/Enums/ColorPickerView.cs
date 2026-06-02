// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NetEscapades.EnumGenerators;

namespace ProtonBlazor;

/// <summary>
/// Indicates the color selection view for a <see cref="ProColorPicker"/>.
/// </summary>
[EnumExtensions]
public enum ColorPickerView
{
    /// <summary>
    /// Colors are chosen from a gradient of colors.
    /// </summary>
    Spectrum,

    /// <summary>
    /// Colors are chosen from a pre-defined palette.
    /// </summary>
    Palette,

    /// <summary>
    /// Colors are chosen from a grid of possible values.
    /// </summary>
    Grid,

    /// <summary>
    /// Colors are chosen from a smaller grid of possible values.
    /// </summary>
    GridCompact
}
