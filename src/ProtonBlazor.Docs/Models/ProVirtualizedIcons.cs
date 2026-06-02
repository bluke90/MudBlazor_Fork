// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.Docs.Models;

#nullable enable
public class ProVirtualizedIcons
{
    public ProIcons[] RowIcons { get; }

    public ProVirtualizedIcons(ProIcons[] rowIcons)
    {
        RowIcons = rowIcons;
    }
}
