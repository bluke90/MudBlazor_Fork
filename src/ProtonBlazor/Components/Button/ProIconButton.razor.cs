using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// A clickable icon for actions and commands.
    /// </summary>
    /// <remarks>
    /// Creates a <see href="https://developer.mozilla.org/docs/Web/HTML/Element/Button">button</see> element,
    /// or <see href="https://developer.mozilla.org/docs/Web/HTML/Element/a">anchor</see> if <c>Href</c> is set.<br/>
    /// You can directly add attributes like <c>title</c> or <c>aria-label</c>.
    /// </remarks>
    /// <seealso cref="ProButton" />
    /// <seealso cref="ProFab" />
    /// <seealso cref="ProToggleIconButton" />
    /// <seealso cref="ProIcon"/>
    public partial class ProIconButton : ProBaseButton
    {
        protected string Classname => new CssBuilder("pro-button-root pro-icon-button")
            .AddClass("pro-button", when: AsButton)
            .AddClass($"pro-{Color.ToStringFast(true)}-text hover:pro-{Color.ToStringFast(true)}-hover", !AsButton && Color != Color.Default)
            .AddClass($"pro-button-{Variant.ToStringFast(true)}", AsButton)
            .AddClass($"pro-button-{Variant.ToStringFast(true)}-{Color.ToStringFast(true)}", AsButton)
            .AddClass($"pro-button-{Variant.ToStringFast(true)}-size-{Size.ToStringFast(true)}", AsButton)
            .AddClass($"pro-ripple", Ripple)
            .AddClass($"pro-ripple-icon", Ripple && !AsButton)
            .AddClass($"pro-icon-button-size-{Size.ToStringFast(true)}", when: () => Size != Size.Medium)
            .AddClass($"pro-icon-button-edge-{Edge.ToStringFast(true)}", when: () => Edge != Edge.False)
            .AddClass($"pro-button-disable-elevation", !DropShadow)
            .AddClass(Class)
            .Build();

        protected bool AsButton => Variant != Variant.Text;

        /// <summary>
        /// The icon to display.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Behavior)]
        public string? Icon { get; set; }

        /// <summary>
        /// The color of the button.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// The size of the button.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Size.Medium"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Size Size { get; set; } = Size.Medium;

        /// <summary>
        /// The amount of negative margin applied.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Edge.False"/>.  Other values are <see cref="Edge.Start"/> and <see cref="Edge.End"/>
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Edge Edge { get; set; }

        /// <summary>
        /// The display variation to use.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Variant.Text"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Appearance)]
        public Variant Variant { get; set; } = Variant.Text;

        /// <summary>
        /// The custom content within this button.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  Only displays if <see cref="Icon"/> is not set.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Button.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
