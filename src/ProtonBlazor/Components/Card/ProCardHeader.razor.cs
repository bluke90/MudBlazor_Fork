using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents the top portion of a <see cref="ProCard"/>.
    /// </summary>
    /// <seealso cref="ProCard" />
    /// <seealso cref="ProCardActions" />
    /// <seealso cref="ProCardContent" />
    /// <seealso cref="ProCardMedia" />
    public partial class ProCardHeader : ProComponentBase
    {
        protected string Classname => new CssBuilder("pro-card-header")
            .AddClass("pro-card-header-padding", ParentCard?.ContentPadding ?? true)
            .AddClass(Class)
            .Build();

        [CascadingParameter]
        private ProCard? ParentCard { get; set; }

        /// <summary>
        /// The avatar to display within this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? CardHeaderAvatar { get; set; }

        /// <summary>
        /// The main content of this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? CardHeaderContent { get; set; }

        /// <summary>
        /// The actions displayed within this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? CardHeaderActions { get; set; }

        /// <summary>
        /// The custom content within this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
