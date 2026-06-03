using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// Represents an item for the <see cref="ProFabMenu"/>.
/// </summary>
public partial class ProFabMenuItem : ProFab
{
    private new string Classname => new CssBuilder(base.Classname)
        .AddClass("pro-fab-menu-item")
        .AddClass(Class)
        .Build();

    /// <summary>
    /// The size of the menu item.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="Size.Medium"/>.
    /// </remarks>
    [Parameter, Category(CategoryTypes.Button.Appearance)]
    public override Size Size { get; set; } = Size.Medium;
}
