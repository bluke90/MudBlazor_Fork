// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// The content within a <see cref="ProPicker{T}"/>.
/// </summary>
/// <seealso cref="ProPicker{T}" />
/// <seealso cref="ProPickerToolbar" />
public partial class ProPickerContent : ProComponentBase
{
    protected string Classname =>
        new CssBuilder("pro-picker-content")
            .AddClass(Class)
            .Build();

    /// <summary>
    /// The content to display.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Picker.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
