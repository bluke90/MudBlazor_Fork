// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using ProtonBlazor.Utilities.Background;

namespace ProtonBlazor.UnitTests.Utilities.Background.Mocks;

internal class WaitForCancelledTokenWorkerMock : BackgroundWorkerBase
{
    public Task ExecutingTask { get; private set; }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ExecutingTask = Task.Delay(Timeout.Infinite, stoppingToken);
        return ExecutingTask;
    }
}
