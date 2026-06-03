// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;


/// <summary>
/// A 12-point grid system for organizing content with responsive breakpoints for different screen sizes.
/// </summary>
/// <seealso cref="ProItem"/>
public partial class ProGrid : ProComponentBase
{
    protected string Classname =>
        new CssBuilder("pro-grid")
            .AddClass($"pro-grid-spacing-xs-{Spacing.ToString()}")
            .AddClass($"justify-{Justify.ToStringFast(true)}")
            .AddClass(Class)
            .Build();

    /// <summary>
    /// The gap between items, measured in increments of <c>4px</c>.
    /// </summary>
    /// <remarks>
    /// <para>Defaults to 6.</para>
    /// <para>Maximum is 20.</para>
    /// <para>The increment was halved in v7, so the default is now 6 instead of 3.</para>
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Grid.Behavior)]
    public int Spacing { set; get; } = 6;

    /// <summary>
    /// Defines the distribution of children along the main axis within a <see cref="ProStack"/> component.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Grid.Behavior)]
    public Justify Justify { get; set; } = Justify.FlexStart;

    /// <summary>
    /// Child content of the component.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Grid.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
