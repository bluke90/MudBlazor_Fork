// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace ProtonBlazor;

public partial class ProAppProgress : ProComponentBase, IAsyncDisposable
{
    [Inject] private IProAppProgressService Progress { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    private IDisposable? _locationChangingHandler;

    private string _barClass => new CssBuilder("pro-app-progress")
        .AddClass("pro-app-progress--complete", Progress.IsComplete)
        .Build();

    protected override void OnInitialized()
    {
        Progress.StateChanged += HandleStateChanged;
        _locationChangingHandler = Navigation.RegisterLocationChangingHandler(OnLocationChanging);
        Navigation.LocationChanged += OnLocationChanged;
    }

    private ValueTask OnLocationChanging(LocationChangingContext context)
    {
        Progress.Start();
        return ValueTask.CompletedTask;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        Progress.Complete();
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    public async ValueTask DisposeAsync()
    {
        Progress.StateChanged -= HandleStateChanged;
        Navigation.LocationChanged -= OnLocationChanged;
        _locationChangingHandler?.Dispose();
        await ValueTask.CompletedTask;
    }
}
