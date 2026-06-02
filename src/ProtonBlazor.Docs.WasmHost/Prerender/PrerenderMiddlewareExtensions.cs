// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Builder;

namespace ProtonBlazor.Docs.WasmHost.Prerender;

public static class PrerenderMiddlewareExtensions
{
    public static IApplicationBuilder UsePrerenderMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PrerenderMiddleware>();
    }
}
