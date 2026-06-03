using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents a floating action button.
    /// </summary>
    /// <remarks>
    /// Creates a <see href="https://developer.mozilla.org/docs/Web/HTML/Element/Button">button</see> element,
    /// or <see href="https://developer.mozilla.org/docs/Web/HTML/Element/a">anchor</see> if <c>Href</c> is set.<br/>
    /// You can directly add attributes like <c>title</c> or <c>aria-label</c>.
    /// </remarks>
    /// <seealso cref="ProButton" />
    /// <seealso cref="ProIconButton" />
    /// <seealso cref="ProToggleIconButton" />
    public partial class ProFab : ProBaseButton
    {
        protected string Classname => new CssBuilder("pro-button-root pro-fab")
            .AddClass($"pro-fab-extended", !string.IsNullOrEmpty(Label))
            .AddClass($"pro-fab-{Color.ToStringFast(true)}")
            .AddClass($"pro-fab-size-{Size.ToStringFast(true)}")
            .AddClass($"pro-ripple", Ripple && !GetDisabledState())
            .AddClass($"pro-fab-disable-elevation", !DropShadow)
            .AddClass(Class)
            .Build();

        /// <summary>
        /// The color of the button.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default"/>.  Theme colors are supported.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// The size of the button.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Size.Large"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public virtual Size Size { get; set; } = Size.Large;

        /// <summary>
        /// The icon shown before any text.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  Use the <see cref="EndIcon"/> property to show an icon after text.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Behavior)]
        public string? StartIcon { get; set; }

        /// <summary>
        /// The icon shown after any text.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  Use the <see cref="StartIcon"/> property to show an icon before text.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Behavior)]
        public string? EndIcon { get; set; }

        /// <summary>
        /// The color of any icons.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Inherit"/>.  Controls the color of <see cref="StartIcon"/> and <see cref="EndIcon"/> icons.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Color IconColor { get; set; } = Color.Inherit;

        /// <summary>
        /// The size of the icon.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Size.Medium"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Size IconSize { get; set; } = Size.Medium;

        /// <summary>
        /// The text displayed in the button.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Behavior)]
        public string? Label { get; set; }
    }
}
