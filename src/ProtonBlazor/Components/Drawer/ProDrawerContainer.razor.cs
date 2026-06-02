using Microsoft.AspNetCore.Components;
using ProtonBlazor.Extensions;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{

    /// <summary>
    /// A container for a <see cref="ProDrawer"/> component.
    /// </summary>
    /// <seealso cref="ProDrawer"/>
    /// <seealso cref="ProDrawerHeader"/>
    public partial class ProDrawerContainer : ProComponentBase
    {
        protected bool Fixed { get; set; } = false;
        private readonly List<ProDrawer> _drawers = new();

        protected virtual string Classname =>
            new CssBuilder()
                .AddClass(GetDrawerClass(FindLeftDrawer()))
                .AddClass(GetDrawerClass(FindRightDrawer()))
                .AddClass(Class)
                .Build();

        protected string Stylename =>
            new StyleBuilder()
                .AddStyle("--pro-drawer-width-left", GetDrawerWidth(FindLeftDrawer()), !string.IsNullOrEmpty(GetDrawerWidth(FindLeftDrawer())))
                .AddStyle("--pro-drawer-width-right", GetDrawerWidth(FindRightDrawer()), !string.IsNullOrEmpty(GetDrawerWidth(FindRightDrawer())))
                .AddStyle("--pro-drawer-height-top", GetDrawerHeight(FindTopDrawer()), !string.IsNullOrEmpty(GetDrawerHeight(FindTopDrawer())))
                .AddStyle("--pro-drawer-height-bottom", GetDrawerHeight(FindBottomDrawer()), !string.IsNullOrEmpty(GetDrawerHeight(FindBottomDrawer())))
                .AddStyle("--pro-drawer-width-mini-left", GetMiniDrawerWidth(FindLeftMiniDrawer()), !string.IsNullOrEmpty(GetMiniDrawerWidth(FindLeftMiniDrawer())))
                .AddStyle("--pro-drawer-width-mini-right", GetMiniDrawerWidth(FindRightMiniDrawer()), !string.IsNullOrEmpty(GetMiniDrawerWidth(FindRightMiniDrawer())))
                .AddStyle(Style)
                .Build();

        /// <summary>
        /// Displays drawers right-to-left.
        /// </summary>
        [CascadingParameter(Name = "RightToLeft")]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// The custom content inside this drawer.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Drawer.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        internal void Add(ProDrawer drawer)
        {
            if (Fixed && !drawer.IsFixed)
                return;

            _drawers.Add(drawer);
            StateHasChanged();
        }

        internal void Remove(ProDrawer drawer)
        {
            _drawers.Remove(drawer);
            StateHasChanged();
        }

        private static string GetDrawerClass(ProDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            var className = $"pro-drawer-{(drawer.GetState<bool>(nameof(ProDrawer.Open)) ? "open" : "close")}-{drawer.Variant.ToStringFast(true)}";
            if (drawer.Variant is DrawerVariant.Responsive or DrawerVariant.Mini)
            {
                className += $"-{drawer.Breakpoint.ToStringFast(true)}";
            }
            className += $"-{drawer.GetPosition()}";

            className += $" pro-drawer-{drawer.GetPosition()}-clipped-{drawer.ClipMode.ToStringFast(true)}";

            return className;
        }

        private static string? GetDrawerWidth(ProDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            return drawer.Width;
        }

        private static string? GetDrawerHeight(ProDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            return drawer.Height;
        }

        private static string? GetMiniDrawerWidth(ProDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            return drawer.MiniWidth;
        }

        private ProDrawer? FindLeftDrawer()
        {
            var anchor = RightToLeft ? Anchor.End : Anchor.Start;

            return _drawers.FirstOrDefault(d => d.Anchor == anchor || d.Anchor == Anchor.Left);
        }

        private ProDrawer? FindRightDrawer()
        {
            var anchor = RightToLeft ? Anchor.Start : Anchor.End;

            return _drawers.FirstOrDefault(d => d.Anchor == anchor || d.Anchor == Anchor.Right);
        }

        private ProDrawer? FindTopDrawer()
        {
            return _drawers.FirstOrDefault(d => d.Anchor == Anchor.Top);
        }

        private ProDrawer? FindBottomDrawer()
        {
            return _drawers.FirstOrDefault(d => d.Anchor == Anchor.Bottom);
        }

        private ProDrawer? FindLeftMiniDrawer()
        {
            var anchor = RightToLeft ? Anchor.End : Anchor.Start;

            return _drawers.FirstOrDefault(d => d.Variant == DrawerVariant.Mini && (d.Anchor == anchor || d.Anchor == Anchor.Left));
        }

        private ProDrawer? FindRightMiniDrawer()
        {
            var anchor = RightToLeft ? Anchor.Start : Anchor.End;

            return _drawers.FirstOrDefault(d => d.Variant == DrawerVariant.Mini && (d.Anchor == anchor || d.Anchor == Anchor.Right));
        }
    }
}
