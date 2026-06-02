// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;


/// <summary>
/// Represents an observer that observes and responds to pointer down events.
/// </summary>
public interface IPointerDownObserver
{
    /// <summary>
    /// Notifies the observer of a pointer down event.
    /// </summary>
    /// <param name="args">The event arguments associated with the pointer down event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task NotifyOnPointerDownAsync(EventArgs args) => Task.CompletedTask;
}
