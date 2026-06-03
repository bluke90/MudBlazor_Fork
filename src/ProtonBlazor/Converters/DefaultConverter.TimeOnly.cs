// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using ProtonBlazor.Resources;
using ProtonBlazor.Utilities.Exceptions;

namespace ProtonBlazor;

internal partial class DefaultConverter
{
    internal sealed class TimeOnlyConverter(Func<CultureInfo> culture, Func<string?> format)
        : IReversibleConverter<TimeOnly, string?>, IReversibleConverter<TimeOnly?, string?>
    {
        public string Convert(TimeOnly input) => input.ToString(format.Invoke(), culture.Invoke());

        public string? Convert(TimeOnly? input) => input?.ToString(format.Invoke(), culture.Invoke());

        public TimeOnly ConvertBack(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default;
            }

            var currentCulture = culture.Invoke();
            if (TimeOnly.TryParseExact(input, format.Invoke() ?? currentCulture.DateTimeFormat.ShortDatePattern, currentCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }

            // TODO: Differentiate error message for TimeOnly
            throw new ConversionException(LanguageResource.Converter_InvalidDateTime);
        }

        TimeOnly? IReversibleConverter<TimeOnly?, string?>.ConvertBack(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            return ConvertBack(input);
        }
    }
}
