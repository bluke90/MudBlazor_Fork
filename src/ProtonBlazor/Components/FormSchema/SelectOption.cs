// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

public sealed class SelectOption
{
    public required string Value { get; init; }
    public required string Label { get; init; }
    public bool Disabled { get; init; }
}
