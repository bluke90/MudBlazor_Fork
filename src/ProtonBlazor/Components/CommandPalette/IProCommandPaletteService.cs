// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Provides a command registry and open/close state for the <see cref="ProCommandPalette"/> component.
/// Register this service with <c>services.AddProtonBlazorCommandPalette()</c> and place
/// <c>&lt;ProCommandPalette /&gt;</c> in your root layout.
/// </summary>
public interface IProCommandPaletteService
{
    /// <summary>Whether the command palette is currently visible.</summary>
    bool IsOpen { get; }

    /// <summary>
    /// Fired whenever <see cref="IsOpen"/> changes or the command list is modified.
    /// The <see cref="ProCommandPalette"/> component subscribes to this to trigger re-renders.
    /// </summary>
    event Action? StateChanged;

    /// <summary>
    /// Registers a command.  If an item with the same <see cref="ProCommandItem.Id"/> already exists
    /// it is replaced in-place (preserving registration order).
    /// </summary>
    void Register(ProCommandItem item);

    /// <summary>Removes the command with the given <paramref name="id"/>. No-op if not found.</summary>
    void Unregister(string id);

    /// <summary>
    /// Returns all commands whose <see cref="ProCommandItem.Label"/> or
    /// <see cref="ProCommandItem.Description"/> contains <paramref name="query"/> (case-insensitive).
    /// Returns all commands when <paramref name="query"/> is <c>null</c> or whitespace.
    /// </summary>
    IReadOnlyList<ProCommandItem> Search(string? query);

    /// <summary>Opens the command palette. No-op if already open.</summary>
    void Open();

    /// <summary>Closes the command palette. No-op if already closed.</summary>
    void Close();

    /// <summary>Toggles the command palette between open and closed.</summary>
    void Toggle();
}
