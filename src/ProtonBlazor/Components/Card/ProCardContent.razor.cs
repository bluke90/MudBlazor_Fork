using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents the primary content displayed within a <see cref="ProCard"/>.
    /// </summary>
    /// <seealso cref="ProCard" />
    /// <seealso cref="ProCardActions" />
    /// <seealso cref="ProCardHeader" />
    /// <seealso cref="ProCardMedia" />
    public partial class ProCardContent : ProComponentBase
    {
        protected string Classname => new CssBuilder("pro-card-content")
            .AddClass("pro-card-content-padding", ParentCard?.ContentPadding ?? true)
            .AddClass(Class)
            .Build();

        [CascadingParameter]
        private ProCard? ParentCard { get; set; }

        /// <summary>
        /// The content within this component.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
