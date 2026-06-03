// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace ProtonBlazor.UnitTests.Services.Popover.Mocks;

#nullable enable
/// <summary>
/// Extended <see cref="PopoverService"/> will the ability to define <see cref="IPopoverTimerMock"/>
/// </summary>
internal class PopoverServiceMock : PopoverService
{
    private readonly IPopoverTimerMock _popoverTimerMock;

    public PopoverServiceMock(ILogger<PopoverService> logger, IJSRuntime jsInterop, TimeProvider timeProvider, IPopoverTimerMock? popoverTimerMock = null, IOptions<PopoverOptions>? options = null)
        : base(logger, jsInterop, timeProvider, options)
    {
        _popoverTimerMock = popoverTimerMock ?? new PopoverTimerEmpty();
    }

    public override async Task OnBatchTimerElapsedAsync(IReadOnlyCollection<ProPopoverHolder> items, CancellationToken cancellationToken)
    {
        await _popoverTimerMock.OnBatchTimerElapsedBeforeAsync(items, cancellationToken).ConfigureAwait(false);
        await base.OnBatchTimerElapsedAsync(items, cancellationToken).ConfigureAwait(false);
        await _popoverTimerMock.OnBatchTimerElapsedAfterAsync(items, cancellationToken).ConfigureAwait(false);
    }

    internal interface IPopoverTimerMock
    {
        Task OnBatchTimerElapsedBeforeAsync(IReadOnlyCollection<ProPopoverHolder> items, CancellationToken cancellationToken);

        Task OnBatchTimerElapsedAfterAsync(IReadOnlyCollection<ProPopoverHolder> items, CancellationToken cancellationToken);
    }

    internal class PopoverTimerEmpty : IPopoverTimerMock
    {
        public Task OnBatchTimerElapsedBeforeAsync(IReadOnlyCollection<ProPopoverHolder> items, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task OnBatchTimerElapsedAfterAsync(IReadOnlyCollection<ProPopoverHolder> items, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
