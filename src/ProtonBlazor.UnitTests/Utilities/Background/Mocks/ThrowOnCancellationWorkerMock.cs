// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using ProtonBlazor.Utilities.Background;

namespace ProtonBlazor.UnitTests.Utilities.Background.Mocks;

internal class ThrowOnCancellationWorkerMock : BackgroundWorkerBase
{
    public int TokenCalls { get; set; }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.Register(() =>
        {
            TokenCalls++;
            throw new InvalidOperationException();
        });

        stoppingToken.Register(() =>
        {
            TokenCalls++;
        });

        return new TaskCompletionSource<object>().Task;
    }
}
