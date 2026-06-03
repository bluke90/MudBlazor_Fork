using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;


/// <summary>
/// A header displayed as part of a <see cref="ProList{T}"/>.
/// </summary>
/// <remarks>
/// Typically used to describe a list.
/// </remarks>
/// <seealso cref="ProList{T}"/>
/// <seealso cref="ProListItem{T}"/>
public partial class ProListSubheader : ProComponentBase
{
    protected string Classname =>
        new CssBuilder("pro-list-subheader")
            .AddClass("pro-list-subheader-gutters", Gutters)
            .AddClass("pro-list-subheader-inset", Inset)
            .AddClass(Class)
            .Build();

    /// <summary>
    /// The content within this header.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.List.Behavior)]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Applies left and right padding to all list items.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.List.Appearance)]
    public bool Gutters { get; set; } = true;

    /// <summary>
    /// Applies an indent to this header.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.List.Appearance)]
    public bool Inset { get; set; }
}
