// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{

    /// <summary>
    /// A list of navigation links with support for groups.
    /// </summary>
    /// <seealso cref="ProNavGroup"/>
    /// <seealso cref="ProNavLink"/>
    public partial class ProNavMenu : ProComponentBase
    {
        protected string Classname =>
            new CssBuilder("pro-navmenu")
                .AddClass($"pro-navmenu-{Color.ToStringFast(true)}")
                .AddClass($"pro-navmenu-margin-{Margin.ToStringFast(true)}")
                .AddClass("pro-navmenu-dense", Dense)
                .AddClass("pro-navmenu-rounded", Rounded)
                .AddClass($"pro-navmenu-bordered pro-border-{Color.ToStringFast(true)}", Bordered)
                .AddClass(Class)
                .Build();

        [CascadingParameter]
        private NavigationContext? NavigationContext { get; set; }

        /// <summary>
        /// The color of the active <see cref="ProNavLink" />.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.NavMenu.Appearance)]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// Shows a border on the active <see cref="ProNavLink"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.NavMenu.Appearance)]
        public bool Bordered { get; set; }

        /// <summary>
        /// Shows a rounded border for all <see cref="ProNavLink" /> items.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// When <c>true</c>, the theme <c>border-radius</c> value will be used. 
        /// Only takes affect if <see cref="Bordered"/> is <c>true</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.NavMenu.Appearance)]
        public bool Rounded { get; set; }

        /// <summary>
        /// The vertical spacing between <see cref="ProNavLink" /> items.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Margin.None"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.NavMenu.Appearance)]
        public Margin Margin { get; set; } = Margin.None;

        /// <summary>
        /// Uses compact vertical padding to all <see cref="ProNavLink"/> items.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.  
        /// Will be overridden if <see cref="Margin"/> is not <see cref="Margin.None"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.NavMenu.Appearance)]
        public bool Dense { get; set; }

        /// <summary>
        /// The content within this menu.
        /// </summary>
        /// <remarks>
        /// Typically contains <see cref="ProNavLink" />, <see cref="ProNavGroup"/>, <see cref="ProText"/>, and <see cref="ProDivider"/> components.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.NavMenu.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
