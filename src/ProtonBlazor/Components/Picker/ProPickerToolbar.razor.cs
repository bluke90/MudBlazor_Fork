// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// The toolbar content of a <see cref="ProPicker{T}"/>.
/// </summary>
/// <seealso cref="ProPicker{T}" />
/// <seealso cref="ProPickerContent" />
public partial class ProPickerToolbar : ProComponentBase
{
    protected string Classname =>
        new CssBuilder("pro-picker-toolbar")
            .AddClass($"pro-theme-{Color.ToStringFast(true)}")
            .AddClass("pro-picker-toolbar-landscape",
                Orientation == Orientation.Landscape && PickerVariant == PickerVariant.Static)
            .AddClass(Class)
            .Build();

    /// <summary>
    /// Shows the toolbar.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Picker.Behavior)]
    public bool ShowToolbar { get; set; } = true;

    /// <summary>
    /// The display orientation of this toolbar.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Picker.Appearance)]
    public Orientation Orientation { get; set; }

    /// <summary>
    /// The display variant for this toolbar.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Picker.Appearance)]
    public PickerVariant PickerVariant { get; set; }

    /// <summary>
    /// The color of the toolbar, selected, and active values.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Picker.Appearance)]
    public Color Color { get; set; }

    /// <summary>
    /// The content within this toolbar.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Picker.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
