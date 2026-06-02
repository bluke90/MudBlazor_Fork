// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ProtonBlazor.Components.Snackbar.InternalComponents;

public partial class SnackbarMessageMarkupString : ComponentBase
{
    /// <summary>
    /// Sets the message to be displayed as HTML content.
    /// </summary>
    /// <remarks>
    /// This property allows you to pass an HTML-formatted message using <see cref="MarkupString"/>.
    /// </remarks>
    [Parameter]
    public MarkupString Message { get; set; }
}
