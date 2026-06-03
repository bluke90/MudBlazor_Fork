// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.UnitTests.Dummy;

public class DummyErrorConverter : IReversibleConverter<int, string>
{
    public string Convert(int input)
    {
        throw new InvalidOperationException("Conversion error");
    }

    public int ConvertBack(string input)
    {
        throw new InvalidOperationException("Conversion error");
    }
}
