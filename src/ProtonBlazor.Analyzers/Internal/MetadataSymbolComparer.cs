// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.Analyzers.Internal;

internal class MetadataSymbolComparer : IEqualityComparer<ISymbol?>
{
    public bool Equals(ISymbol? x, ISymbol? y)
    {
        if (x is null || y is null)
            return false;

        return x.MetadataName.Equals(y.MetadataName);
    }

    public int GetHashCode(ISymbol? obj)
    {
        return base.GetHashCode();
    }
}
