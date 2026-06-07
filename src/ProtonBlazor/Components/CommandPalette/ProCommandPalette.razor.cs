// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// A Ctrl+K / Cmd+K command palette that lets users search and execute registered commands.
/// Place this component once in your root layout (e.g. <c>MainLayout.razor</c>) and register
/// commands via <see cref="IProCommandPaletteService"/>.
/// </summary>
public partial class ProCommandPalette : ProComponentBase, IAsyncDisposable
{
    [Inject]
    private IProCommandPaletteService CommandPalette { get; set; } = null!;

    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    private ElementReference _inputRef;
    private DotNetObjectReference<ProCommandPalette>? _dotnetRef;

    private string _query = string.Empty;
    private int _activeIndex;
    private IReadOnlyList<ProCommandItem> _filtered = [];

    protected override void OnInitialized()
    {
        _filtered = CommandPalette.Search(null);
        CommandPalette.StateChanged += HandleStateChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotnetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("proCommandPalette.initialize", _dotnetRef);
        }

        if (CommandPalette.IsOpen)
        {
            try { await _inputRef.FocusAsync(); }
            catch { /* input may not yet be in the DOM on this render pass */ }
        }
    }

    private void HandleStateChanged()
    {
        if (!CommandPalette.IsOpen)
        {
            _query = string.Empty;
            _activeIndex = 0;
        }

        _filtered = CommandPalette.Search(_query);
        InvokeAsync(StateHasChanged);
    }

    private void OnInput(ChangeEventArgs e)
    {
        _query = e.Value?.ToString() ?? string.Empty;
        _activeIndex = 0;
        _filtered = CommandPalette.Search(_query);
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        switch (e.Key)
        {
            case "Escape":
                CommandPalette.Close();
                break;

            case "ArrowDown":
                _activeIndex = Math.Min(_activeIndex + 1, _filtered.Count - 1);
                StateHasChanged();
                break;

            case "ArrowUp":
                _activeIndex = Math.Max(_activeIndex - 1, 0);
                StateHasChanged();
                break;

            case "Enter":
                if (_activeIndex >= 0 && _activeIndex < _filtered.Count)
                    await ExecuteAsync(_filtered[_activeIndex]);
                break;
        }
    }

    private void HandleBackdropClick() => CommandPalette.Close();

    private async Task ExecuteAsync(ProCommandItem item)
    {
        CommandPalette.Close();
        await item.Action();
    }

    private string GetItemClass(int index) =>
        new CssBuilder("pro-command-palette-item")
            .AddClass("pro-command-palette-item--active", index == _activeIndex)
            .Build();

    /// <summary>Called by the global JS listener to toggle the palette (Ctrl+K / Cmd+K).</summary>
    [JSInvokable]
    public void HandleToggle() => CommandPalette.Toggle();

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        CommandPalette.StateChanged -= HandleStateChanged;

        try { await JS.InvokeVoidAsync("proCommandPalette.dispose"); }
        catch (JSDisconnectedException) { /* component torn down during circuit disconnect */ }

        _dotnetRef?.Dispose();
    }
}
