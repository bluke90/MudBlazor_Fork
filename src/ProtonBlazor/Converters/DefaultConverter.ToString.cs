// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

internal partial class DefaultConverter
{
    internal sealed class ToStringFallbackConverter<T> : IReversibleConverter<T?, string?>
    {
        public string? Convert(T? input) => input?.ToString();

        public T? ConvertBack(string? input) => default;
    }
}
