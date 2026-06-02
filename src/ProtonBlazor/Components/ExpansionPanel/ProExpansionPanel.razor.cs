using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.State;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{

    /// <summary>
    /// A component which can be expanded to show more content or collapsed to show only its header.
    /// </summary>
    /// <remarks>
    /// This component is always inside a <see cref="ProExpansionPanels"/> component.
    /// </remarks>
    /// <seealso cref="ProExpansionPanels"/>
    /// <seealso cref="ProCollapse"/>
    public partial class ProExpansionPanel : ProComponentBase, IDisposable
    {
        internal readonly ParameterState<bool> _expandedState;

        [CascadingParameter]
        private ProExpansionPanels? Parent { get; set; }

        protected string Classname =>
            new CssBuilder("pro-expand-panel")
                .AddClass("pro-panel-expanded", _expandedState.Value)
                .AddClass("pro-panel-next-expanded", NextPanelExpanded)
                .AddClass("pro-disabled", Disabled)
                .AddClass($"pro-elevation-{Parent?.Elevation.ToString()}")
                .AddClass($"pro-expand-panel-border", Parent?.Outlined == true)
                .AddClass(Class)
                .Build();

        protected string HeaderClassname =>
            new CssBuilder("pro-expand-panel-header")
                .AddClass("pro-expand-panel-header-gutters", Gutters && Parent?.Gutters != false)
                .AddClass(HeaderClass)
                .Build();

        protected string PanelContentClassname =>
            new CssBuilder("pro-expand-panel-content")
                .AddClass("pro-expand-panel-gutters", Gutters && Parent?.Gutters != false)
                .AddClass("pro-expand-panel-dense", Dense || Parent?.Dense == true)
                .Build();

        /// <summary>
        /// The maximum allowed height, in pixels.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  When <c>null</c>, the CSS default is used for maximum height.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public int? MaxHeight { get; set; }

        /// <summary>
        /// User class names, separated by space.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public string? HeaderClass { get; set; }

        /// <summary>
        /// The content within the title area.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  When set, overrides the <see cref="Text"/> property.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public RenderFragment? TitleContent { get; set; }

        /// <summary>
        /// The text displayed in this panel, if <see cref="TitleContent"/> is not set.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public string? Text { get; set; }

        /// <summary>
        /// Hides the expand icon.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool HideIcon { get; set; }

        /// <summary>
        /// The icon for expanding this panel.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Icons.Material.Filled.ExpandMore"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public string Icon { get; set; } = Icons.Material.Filled.ExpandMore;

        /// <summary>
        /// Removes vertical padding from the panel.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool Dense { get; set; }

        /// <summary>
        /// Adds left and right padding.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool Gutters { get; set; } = true;

        /// <summary>
        /// Occurs when <see cref="Expanded"/> has changed.
        /// </summary>
        [Parameter]
        public EventCallback<bool> ExpandedChanged { get; set; }

        /// <summary>
        /// Displays the panel content.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public bool Expanded { get; set; }

        /// <summary>
        /// Disables user interaction and prevents <see cref="ToggleExpansionAsync"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public bool Disabled { get; set; }

        /// <summary>
        /// The content within this panel.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// When true, the content remains in the DOM even when the panel is collapsed.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public bool KeepContentAlive { get; set; } = true;

        /// <summary>
        /// The next panel is currently expanded.
        /// </summary>
        public bool NextPanelExpanded { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ProExpansionPanel()
        {
            using var registerScope = CreateRegisterScope();
            _expandedState = registerScope.RegisterParameter<bool>(nameof(Expanded))
                .WithParameter(() => Expanded)
                .WithEventCallback(() => ExpandedChanged)
                .WithChangeHandler(OnExpandedParameterChangedAsync);
        }

        private Task OnExpandedParameterChangedAsync(ParameterChangedEventArgs<bool> args)
        {
            if (Parent is null)
            {
                return Task.CompletedTask;
            }

            return Parent.NotifyPanelsChanged(this);
        }

        /// <summary>
        /// Shows or hides the content in this panel.
        /// </summary>
        /// <remarks>
        /// If <see cref="Disabled"/> is <c>true</c>, this method has no affect.
        /// </remarks>
        public async Task ToggleExpansionAsync()
        {
            if (Disabled)
            {
                return;
            }

            await _expandedState.SetValueAsync(!_expandedState.Value);
            if (Parent is not null)
            {
                await Parent.NotifyPanelsChanged(this);
            }
        }

        /// <summary>
        /// Shows the content in this panel.
        /// </summary>
        public async Task ExpandAsync()
        {
            await _expandedState.SetValueAsync(true);
            if (Parent is not null)
            {
                await Parent.NotifyPanelsChanged(this);
            }
        }

        /// <summary>
        /// Hides the content in this panel.
        /// </summary>
        public async Task CollapseAsync()
        {
            await _expandedState.SetValueAsync(false);
            if (Parent is not null)
            {
                await Parent.NotifyPanelsChanged(this);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            // NOTE: we can't throw here because we need to be able to instantiate the type for the API Docs to infer default values
            //if (Parent == null)
            //    throw new ArgumentNullException(nameof(Parent), "ExpansionPanel must exist within a ExpansionPanels component");
            await base.OnInitializedAsync();
            if (Parent is not null)
            {
                await Parent.AddPanelAsync(this);
            }
        }

        /// <summary>
        /// Releases resources used by this panel.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Parent?.RemovePanel(this);
            }
        }

        private async Task HandleKeyDownAsync(KeyboardEventArgs e)
        {
            if (Disabled)
            {
                return;
            }

            if (e.Key == "Enter" || e.Key == " ")
            {
                await ToggleExpansionAsync();
            }
        }
    }
}
