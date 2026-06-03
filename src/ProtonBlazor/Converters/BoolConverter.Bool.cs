// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor;

internal partial class BoolConverter
{
    internal sealed class BoolIdentity : IReversibleConverter<bool, bool?>, IReversibleConverter<bool?, bool?>
    {
        public bool? Convert(bool value) => value;

        public bool? Convert(bool? value) => value;

        bool IReversibleConverter<bool, bool?>.ConvertBack(bool? value) => value == true;

        public bool? ConvertBack(bool? value) => value;

        public static BoolIdentity Instance { get; } = new();
    }
}
