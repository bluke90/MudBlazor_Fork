// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

/// <inheritdoc cref="IProCommandPaletteService"/>
public sealed class ProCommandPaletteService : IProCommandPaletteService
{
    private readonly List<ProCommandItem> _commands = [];

    /// <inheritdoc/>
    public bool IsOpen { get; private set; }

    /// <inheritdoc/>
    public event Action? StateChanged;

    /// <inheritdoc/>
    public void Register(ProCommandItem item)
    {
        var idx = _commands.FindIndex(c => c.Id == item.Id);
        if (idx >= 0)
            _commands[idx] = item;
        else
            _commands.Add(item);

        StateChanged?.Invoke();
    }

    /// <inheritdoc/>
    public void Unregister(string id)
    {
        if (_commands.RemoveAll(c => c.Id == id) > 0)
            StateChanged?.Invoke();
    }

    /// <inheritdoc/>
    public IReadOnlyList<ProCommandItem> Search(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return _commands.AsReadOnly();

        return _commands
            .Where(c =>
                c.Label.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) == true)
            .ToList()
            .AsReadOnly();
    }

    /// <inheritdoc/>
    public void Open()
    {
        if (IsOpen) return;
        IsOpen = true;
        StateChanged?.Invoke();
    }

    /// <inheritdoc/>
    public void Close()
    {
        if (!IsOpen) return;
        IsOpen = false;
        StateChanged?.Invoke();
    }

    /// <inheritdoc/>
    public void Toggle()
    {
        if (IsOpen) Close(); else Open();
    }
}
