// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.Services;

namespace ProtonBlazor;

/// <summary>
/// Represents an observer for browser viewport updates.
/// </summary>
public interface IBrowserViewportObserver
{
    /// <summary>
    /// Gets the unique identifier of the observer.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the resize options for the observer.
    /// When set to null, the global options provided during AddMudServices / AddProtonBlazorResizeListener will be used.
    /// When specific options are provided, they will be used to observe the changes.
    /// </summary>
    /// <remarks>
    /// After you set the options, modifying the instance won't have any effect, including re-subscription, as C# and JS side doesn't support this, you need to <see cref="IBrowserViewportService.UnsubscribeAsync(IBrowserViewportObserver)"/> and subscribe again.
    /// </remarks>
    ResizeOptions? ResizeOptions => null;

    /// <summary>
    /// Notifies the observer of browser size and breakpoint change.
    /// </summary>
    /// <param name="browserViewportEventArgs">The event arguments containing information about the <see cref="BrowserWindowSize"/> and <see cref="Breakpoint"/> change.</param>
    /// <returns>A task representing the asynchronous notification operation.</returns>
    Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs);
}
