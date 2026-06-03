using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents a set of buttons displayed as part of a <see cref="ProCard"/>.
    /// </summary>
    /// <seealso cref="ProCard" />
    /// <seealso cref="ProCardContent" />
    /// <seealso cref="ProCardHeader" />
    /// <seealso cref="ProCardMedia" />
    public partial class ProCardActions : ProComponentBase
    {
        protected string Classname => new CssBuilder("pro-card-actions")
            .AddClass("pro-card-actions-padding", ParentCard?.ContentPadding ?? true)
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
