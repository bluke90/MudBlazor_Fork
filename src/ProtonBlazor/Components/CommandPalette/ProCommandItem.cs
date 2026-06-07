// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <summary>
/// Represents a single command that can appear in the <see cref="ProCommandPalette"/>.
/// </summary>
public sealed class ProCommandItem
{
    /// <summary>Unique identifier used to register and unregister the command.</summary>
    public required string Id { get; init; }

    /// <summary>The primary display label shown in the palette list.</summary>
    public required string Label { get; init; }

    /// <summary>Optional secondary description shown below the label.</summary>
    public string? Description { get; init; }

    /// <summary>
    /// Optional Material icon name displayed to the left of the label.
    /// Use values from <see cref="ProIcons.Material.Filled"/>, e.g. <c>ProIcons.Material.Filled.Settings</c>.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>Optional group/category header used to visually separate related commands.</summary>
    public string? Group { get; init; }

    /// <summary>
    /// Optional human-readable keyboard shortcut displayed on the right side of the item,
    /// e.g. <c>"⌘K"</c> or <c>"Ctrl+S"</c>.  This is display-only; use <see cref="IProCommandPaletteService"/>
    /// registration to wire up the actual shortcut.
    /// </summary>
    public string? Shortcut { get; init; }

    /// <summary>The async action invoked when the user selects this command.</summary>
    public required Func<Task> Action { get; init; }
}
