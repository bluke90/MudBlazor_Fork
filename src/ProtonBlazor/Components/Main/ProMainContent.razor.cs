// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// Represents the main content area of the <see cref="ProLayout"/>.
/// </summary>
public partial class ProMainContent : ProComponentBase
{
    /// <summary>
    /// Gets the CSS class names for the component.
    /// </summary>
    protected string Classname =>
        new CssBuilder("pro-main-content")
            .AddClass(Class)
            .Build();

    /// <summary>
    /// Sets the content to be rendered inside the main content area.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.MainContent.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
