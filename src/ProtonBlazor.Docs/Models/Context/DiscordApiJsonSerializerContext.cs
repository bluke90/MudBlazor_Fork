// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace ProtonBlazor.Docs.Models.Context
{
    [JsonSerializable(typeof(DiscordInvite))]
    public sealed partial class DiscordApiJsonSerializerContext : JsonSerializerContext
    {
    }
}
