// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public interface IServerDataAdapter<TItem>
{
    Task<ServerDataResult<TItem>> GetDataAsync(ServerDataRequest request, CancellationToken cancellationToken = default);
}
