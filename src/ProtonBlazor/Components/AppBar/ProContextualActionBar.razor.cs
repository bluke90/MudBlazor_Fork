// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.State;

namespace ProtonBlazor;


/// <summary>
/// A contextual app bar.
/// </summary>
/// <seealso cref="ProAppBar"/>
public partial class ProContextualActionBar : ProAppBar
{
    private new bool Contextual { get; set; }

    private RenderFragment ContextualContent => base.BuildRenderTree;

    /// <summary>
    /// Determines if the action bar is visible.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c>.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    [Category(CategoryTypes.Overlay.Behavior)]
    public bool Visible { get; set; }
}
