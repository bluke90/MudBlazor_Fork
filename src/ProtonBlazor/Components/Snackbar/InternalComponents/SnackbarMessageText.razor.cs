// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ProtonBlazor.Components.Snackbar.InternalComponents;

public partial class SnackbarMessageText : ComponentBase
{
    /// <summary>
    /// Plain text message to be displayed.
    /// </summary>
    /// <remarks>
    /// This property is used to pass a plain string message. It does not support HTML or UI fragments.
    /// </remarks>
    [Parameter]
    public string? Message { get; set; }
}
