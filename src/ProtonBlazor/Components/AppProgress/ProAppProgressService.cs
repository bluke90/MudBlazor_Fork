// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class ProAppProgressService : IProAppProgressService
{
    public bool IsVisible { get; private set; }
    public bool IsComplete { get; private set; }
    public event Action? StateChanged;

    public void Start()
    {
        IsVisible = true;
        IsComplete = false;
        StateChanged?.Invoke();
    }

    public void Complete()
    {
        if (!IsVisible) return;

        IsComplete = true;
        StateChanged?.Invoke();

        // After the CSS fade-out transition finishes, hide the bar entirely.
        _ = Task.Delay(450).ContinueWith(_ =>
        {
            IsVisible = false;
            IsComplete = false;
            StateChanged?.Invoke();
        }, TaskScheduler.Default);
    }
}
