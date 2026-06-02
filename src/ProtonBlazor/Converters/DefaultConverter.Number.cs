// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Numerics;
using ProtonBlazor.Resources;
using ProtonBlazor.Utilities.Exceptions;

namespace ProtonBlazor;

internal partial class DefaultConverter
{
    internal sealed class NumberConverter<TNumber>(Func<CultureInfo> culture, Func<string?> format)
        : IReversibleConverter<TNumber, string?>
        where TNumber : INumber<TNumber>
    {
        public string Convert(TNumber input) => input.ToString(format.Invoke(), culture.Invoke());

        public TNumber ConvertBack(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return TNumber.Zero;
            }

            var currentCulture = culture.Invoke();

            if (TNumber.TryParse(input, NumberStyles.Any, currentCulture, out var result))
            {
                return result;
            }

            throw new ConversionException(LanguageResource.Converter_InvalidNumber);
        }
    }
}
