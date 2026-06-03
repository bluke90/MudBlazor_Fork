// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.Docs.Models;

#nullable enable
public class ProIcons
{
    public string Name { get; }

    public string Code { get; }

    public string Category { get; }

    public ProIcons(string name, string code, string category)
    {
        Name = name;
        Code = code;
        Category = category;
    }

    public static readonly ProIcons Empty = new(string.Empty, string.Empty, string.Empty);
}
