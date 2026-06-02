// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.Utilities;

namespace ProtonBlazor;


/// <summary>
/// A component for breaking a flex display using CSS styles.
/// </summary>
public partial class ProFlexBreak : ProComponentBase
{
    /// <summary>
    /// Class names separated by spaces.
    /// </summary>
    protected string Classname =>
        new CssBuilder("pro-flex-break")
            .AddClass(Class)
            .Build();
}
