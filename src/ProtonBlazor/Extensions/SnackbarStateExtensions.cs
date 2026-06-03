// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

internal static class SnackbarStateExtensions
{
    public static bool IsShowing(this SnackbarState state) => state == SnackbarState.Showing;

    public static bool IsVisible(this SnackbarState state) => state == SnackbarState.Visible;

    public static bool IsHiding(this SnackbarState state) => state == SnackbarState.Hiding;
}
