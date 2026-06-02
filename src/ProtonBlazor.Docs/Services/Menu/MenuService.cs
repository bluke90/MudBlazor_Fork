// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.Charts;
using ProtonBlazor.Docs.Models;

namespace ProtonBlazor.Docs.Services
{
#nullable enable
    /// <summary>
    /// The aim of this class is to add new items to NavMenu
    /// </summary>
    public class MenuService : IMenuService
    {
        private IEnumerable<DocsLink>? _features;
        private IEnumerable<DocsLink>? _utilities;
        private DocsComponents? _docsComponentsApi; //cached property
        private IEnumerable<DocsLink>? _customization;
        private readonly Dictionary<Type, ProComponent> _parents = [];
        private readonly Dictionary<Type, ProComponent> _componentLookup = [];

        /// <summary>
        /// Here is where the links for the Components Menu in NavMenu are added
        /// Add here the new menu elements without caring about the order.
        /// They will be reordered automatically
        /// </summary>
        private readonly List<ProComponent> _docsComponents = new DocsComponents()
            //Individual elements
            .AddItem("Container", typeof(ProContainer))
            .AddItem("Grid", typeof(ProGrid), typeof(ProItem))
            .AddItem("Hidden", typeof(ProHidden))
            .AddItem("Breakpoint Provider", typeof(ProBreakpointProvider))
            .AddItem("Chips", typeof(ProChip<T>))
            .AddItem("Chip Set", typeof(ProChipSet<T>))
            .AddItem("Badge", typeof(ProBadge))
            .AddItem("App Bar", typeof(ProAppBar))
            .AddItem("Drawer", typeof(ProDrawer), typeof(ProDrawerHeader), typeof(ProDrawerContainer))
            .AddItem("Drop Zone", typeof(ProDropZone<T>), typeof(ProDropContainer<T>), typeof(ProDynamicDropItem<T>))
            .AddItem("Link", typeof(ProLink))
            .AddItem("Menu", typeof(ProMenu), typeof(ProMenuItem))
            .AddItem("Message Box", typeof(ProMessageBox))
            .AddItem("Nav Menu", typeof(ProNavMenu), typeof(ProNavLink), typeof(ProNavGroup))
            .AddItem("Tabs", typeof(ProTabs), typeof(ProTabPanel), typeof(ProDynamicTabs))
            .AddItem("Progress", typeof(ProProgressCircular), typeof(ProProgressLinear))
            .AddItem("Dialog", typeof(ProDialog), typeof(ProDialogContainer), typeof(ProDialogProvider))
            .AddItem("Snackbar", typeof(SnackbarService), typeof(ProSnackbarProvider), typeof(ProSnackbarElement))
            .AddItem("Avatar", typeof(ProAvatar), typeof(ProAvatarGroup))
            .AddItem("Alert", typeof(ProAlert))
            .AddItem("Card", typeof(ProCard), typeof(ProCardActions), typeof(ProCardContent), typeof(ProCardHeader), typeof(ProCardMedia))
            .AddItem("Divider", typeof(ProDivider))
            .AddItem("Expansion Panels", typeof(ProExpansionPanels), typeof(ProExpansionPanel))
            .AddItem("Image", typeof(ProImage))
            .AddItem("Icons", typeof(ProIcon))
            .AddItem("List", typeof(ProList<T>), typeof(ProListItem<T>), typeof(ProListSubheader))
            .AddItem("Paper", typeof(ProPaper))
            .AddItem("Rating", typeof(ProRating), typeof(ProRatingItem))
            .AddItem("Skeleton", typeof(ProSkeleton))
            .AddItem("Table", typeof(ProTable<T>), typeof(ProTableBase), typeof(ProTablePager), typeof(ProTableGroupRow<T>), typeof(ProTableSortLabel<T>), typeof(ProTd), typeof(ProTh), typeof(ProTr), typeof(ProTFootRow), typeof(ProTHeadRow))
            .AddItem("Data Grid", typeof(ProDataGrid<T>), typeof(Column<T>), typeof(FilterHeaderCell<T>), typeof(FooterCell<T>), typeof(HeaderCell<T>), typeof(HierarchyColumn<T>), typeof(ProDataGridPager<T>), typeof(TemplateColumn<T>))
            .AddItem("Simple Table", typeof(ProSimpleTable))
            .AddItem("Tooltip", typeof(ProTooltip))
            .AddItem("Typography", typeof(ProText))
            .AddItem("Overlay", typeof(ProOverlay))
            .AddItem("Highlighter", typeof(ProHighlighter))
            .AddItem("Element", typeof(ProElement))
            .AddItem("Focus Trap", typeof(ProFocusTrap))
            .AddItem("Tree View", typeof(ProTreeView<T>), typeof(ProTreeViewItem<T>), typeof(ProTreeViewItemToggleButton))
            .AddItem("Breadcrumbs", typeof(ProBreadcrumbs))
            .AddItem("Scroll To Top", typeof(ProScrollToTop))
            .AddItem("Popover", typeof(ProPopover))
            .AddItem("Swipe Area", typeof(ProSwipeArea))
            .AddItem("Tool Bar", typeof(ProToolBar))
            .AddItem("Carousel", typeof(ProCarousel<T>), typeof(ProCarouselItem))
            .AddItem("Timeline", typeof(ProTimeline), typeof(ProTimelineItem))
            .AddItem("Pagination", typeof(ProPagination))
            .AddItem("Stack", typeof(ProStack))
            .AddItem("Spacer", typeof(ProSpacer))
            .AddItem("Collapse", typeof(ProCollapse))
            .AddItem("Stepper", typeof(ProStepper), typeof(ProStep))
            .AddItem("Split Panel", typeof(ProSplitPanel))

            //GROUPS

            //Inputs
            .AddNavGroup("Form & Inputs", false, new DocsComponents()
                .AddItem("Radio", typeof(ProRadio<T>), typeof(ProRadioGroup<T>))
                .AddItem("Check Box", typeof(ProCheckBox<T>))
                .AddItem("Select", typeof(ProSelect<T>), typeof(ProSelectItem<T>))
                .AddItem("Slider", typeof(ProSlider<T>))
                .AddItem("Switch", typeof(ProSwitch<T>))
                .AddItem("Text Field", typeof(ProTextField<T>))
                .AddItem("Numeric Field", typeof(ProNumericField<T>))
                .AddItem("Form", typeof(ProForm))
                .AddItem("Autocomplete", typeof(ProAutocomplete<T>))
                .AddItem("Field", typeof(ProField))
                .AddItem("File Upload", typeof(ProFileUpload<T>))
                .AddItem("Toggle Group", typeof(ProToggleGroup<T>), typeof(ProToggleItem<T>))
            )

            //Pickers
            .AddNavGroup("Pickers", false, new DocsComponents()
                .AddItem("Date Picker", typeof(ProDatePicker))
                .AddItem("Date Range Picker", typeof(ProDateRangePicker))
                .AddItem("Time Picker", typeof(ProTimePicker))
                .AddItem("Color Picker", typeof(ProColorPicker))
            )

            //Buttons
            .AddNavGroup("Buttons", false, new DocsComponents()
                .AddItem("Button", typeof(ProButton))
                .AddItem("Button Group", typeof(ProButtonGroup))
                .AddItem("Icon Button", typeof(ProIconButton))
                .AddItem("Toggle Icon Button", typeof(ProToggleIconButton))
                .AddItem("Button FAB", typeof(ProFab))
                .AddItem("Button FAB Menu", typeof(ProFabMenu))
            )

            //Charts
            .AddNavGroup("Charts", false, new DocsComponents()
                .AddItem("Donut Chart", typeof(Donut<T>), typeof(DonutChartOptions), typeof(Legend<T>))
                .AddItem("Line Chart", typeof(Line<T>), typeof(LineChartOptions), typeof(Legend<T>))
                .AddItem("Pie Chart", typeof(Pie<T>), typeof(PieChartOptions), typeof(Legend<T>))
                .AddItem("Bar Chart", typeof(Bar<T>), typeof(BarChartOptions), typeof(Legend<T>))
                .AddItem("Heat Map Chart", typeof(HeatMap<T>), typeof(HeatMapChartOptions), typeof(Legend<T>))
                .AddItem("Stacked Bar Chart", typeof(StackedBar<T>), typeof(StackedBarChartOptions), typeof(Legend<T>))
                .AddItem("Time Series Chart", typeof(TimeSeries<T>), typeof(TimeSeriesChartOptions), typeof(Legend<T>))
                .AddItem("Radar Chart", typeof(Radar<T>), typeof(RadarChartOptions), typeof(Legend<T>))
                .AddItem("Rose Chart", typeof(Rose<T>), typeof(RoseChartOptions), typeof(Legend<T>))
                .AddItem("Sankey Chart", typeof(Sankey<T>), typeof(SankeyChartOptions), typeof(Legend<T>))
                .AddItem("Scatter Plot Chart", typeof(ScatterPlot<T>), typeof(ScatterPlotChartOptions), typeof(Legend<T>))
                .AddItem("Universal Chart", typeof(ProChart<T>), typeof(ProAxisChartBase<,>), typeof(ChartOptions))
            )

            // Functional
            .AddNavGroup("Functional", false, new DocsComponents()
                .AddItem("Exit Prompt", typeof(ProExitPrompt))
                .AddItem("Hotkey", typeof(ProHotkey))
            )

            // this must be last!
            .GetComponentsSortedByName();

        /// <summary>
        /// Features menu links
        /// </summary>
        public IEnumerable<DocsLink> Features => _features ??= new List<DocsLink>
        {
            new DocsLink { Title = "Breakpoints", Href = "features/breakpoints" },
            new DocsLink { Title = "Colors", Href = "features/colors" },
            new DocsLink { Title = "Elevation", Href = "features/elevation" },
            new DocsLink { Title = "Converters", Href = "features/converters" },
            new DocsLink { Title = "Icon Reference", Href = "features/icons" }, // <-- note: title changed from "Icons" to "Icon Reference" to avoid confusion in Search box with the ProIcon page which is also called "Icons"
            new DocsLink { Title = "Parameter State", Href = "features/parameterstate" },
            new DocsLink { Title = "Masking", Href = "features/masking" },
            new DocsLink { Title = "RTL Languages", Href = "features/rtl-languages" },
            new DocsLink { Title = "Localization", Href = "features/localization" },
            new DocsLink { Title = "Analyzers", Href = "features/analyzers" },
            new DocsLink { Title = "Services", Href = "features/services" },
            new DocsLink { Title = "Chat (deprecated)", Href = "components/chat" }, // TODO: there is no component to reference so it's added under features instead so the page is still searchable. remove this in v10.
        }.OrderBy(x => x.Title);

        /// <summary>
        /// Customization menu links
        /// </summary>
        public IEnumerable<DocsLink> Customization => _customization ??= new List<DocsLink>
        {
            new DocsLink { Title = "Default theme", Href = "customization/default-theme" },
            new DocsLink { Title = "Overview", Href = "customization/overview" },
            new DocsLink { Title = "Palette", Href = "customization/palette" },
            new DocsLink { Title = "Typography", Href = "customization/typography" },
            new DocsLink { Title = "z-index", Href = "customization/z-index" },
            new DocsLink { Title = "Pseudo CSS", Href = "customization/pseudocss" },
            new DocsLink { Title = "Globals", Href = "customization/globals", Order = 100 },
        }.OrderBy(link => link.Order).ThenBy(x => x.Title);

        /// <summary>
        /// CSS Utilities menu links
        /// </summary>
        public IEnumerable<DocsLink> Utilities => _utilities ??= new List<DocsLink>
        {
            new DocsLink { Group = "Layout", Title = "Display", Href = "utilities/display" },
            new DocsLink { Group = "Layout", Title = "Z-Index", Href = "utilities/z-index" },
            new DocsLink { Group = "Layout", Title = "Overflow", Href = "utilities/overflow" },
            new DocsLink { Group = "Layout", Title = "Visibility", Href = "utilities/visibility" },
            new DocsLink { Group = "Layout", Title = "Object Fit", Href = "utilities/object-fit" },
            new DocsLink { Group = "Layout", Title = "Object Position", Href = "utilities/object-position" },
            new DocsLink { Group = "Layout", Title = "Position", Href = "utilities/position" },

            new DocsLink { Group = "Flexbox", Title = "Enable Flexbox", Href = "utilities/enable-flex" },
            new DocsLink { Group = "Flexbox", Title = "Flex Direction", Href = "utilities/flex-direction" },
            new DocsLink { Group = "Flexbox", Title = "Flex Wrap", Href = "utilities/flex-wrap" },
            new DocsLink { Group = "Flexbox", Title = "Flex", Href = "utilities/flex" },
            new DocsLink { Group = "Flexbox", Title = "Flex Grow", Href = "utilities/flex-grow" },
            new DocsLink { Group = "Flexbox", Title = "Flex Shrink", Href = "utilities/flex-shrink" },
            new DocsLink { Group = "Flexbox", Title = "Order", Href = "utilities/order" },
            new DocsLink { Group = "Flexbox", Title = "Gap", Href = "utilities/gap" },
            new DocsLink { Group = "Flexbox", Title = "Justify Content", Href = "utilities/justify-content" },
            new DocsLink { Group = "Flexbox", Title = "Align Content", Href = "utilities/align-content" },
            new DocsLink { Group = "Flexbox", Title = "Align Items", Href = "utilities/align-items" },
            new DocsLink { Group = "Flexbox", Title = "Align Self", Href = "utilities/align-self" },

            new DocsLink { Group = "Spacing", Title = "Spacing", Href = "utilities/spacing" },

            new DocsLink { Group = "Borders", Title = "Border Radius", Href = "utilities/border-radius" },
            new DocsLink { Group = "Borders", Title = "Border Style", Href = "utilities/border-style" },
            new DocsLink { Group = "Borders", Title = "Border Width", Href = "utilities/border-width" },

            new DocsLink { Group = "Interactivity", Title = "Cursor", Href = "utilities/cursor" },
            new DocsLink { Group = "Interactivity", Title = "Pointer Events", Href = "utilities/pointer-events" },
        };

        public IEnumerable<ProComponent> Components => _docsComponents;

        public IEnumerable<ProComponent> Api => DocsComponentsApi.Components;

        public MenuService()
        {
            foreach (var component in Components)
            {
                if (component.IsNavGroup)
                {
                    foreach (var groupComponent in component.GroupComponents)
                    {
                        _componentLookup.Add(groupComponent.Type, groupComponent);
                        _parents.Add(groupComponent.Type, component);
                    }
                }
                else
                {
                    _componentLookup.Add(component.Type, component);
                    // top-level types refer to themself as parent ;)
                    _parents.Add(component.Type, component);
                    if (component.ChildTypes is not null)
                    {
                        foreach (var childType in component.ChildTypes)
                        {
                            _parents.Add(childType, component);
                        }
                    }
                }
            }
        }

        public ProComponent? GetParent(Type? child)
        {
            return child is not null
                ? _parents.GetValueOrDefault(child)
                : null;
        }

        public ProComponent? GetComponent(Type? type)
        {
            if (type is null)
            {
                return null;
            }

            return _componentLookup.TryGetValue(type, out var component)
                ? component
                : _parents.GetValueOrDefault(type);
        }

        /// <inheritdoc />
        public string? GetComponentName(string typeName)
        {
            var cleanName = typeName.Replace("`1", "<T>").Replace("`2", "<T, U>");
            foreach (var component in _docsComponents)
            {
                if (component.ComponentName != null && component.ComponentName.Equals(cleanName, StringComparison.OrdinalIgnoreCase))
                {
                    return component.Name;
                }

                if (component.GroupComponents != null)
                {
                    foreach (var groupComponent in component.GroupComponents)
                    {
                        if (groupComponent.ComponentName.Equals(cleanName, StringComparison.OrdinalIgnoreCase))
                        {
                            return groupComponent.Name;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// This autogenerates the Menu for the API
        /// </summary>
        private DocsComponents DocsComponentsApi
        {
            get
            {
                //caching property
                if (_docsComponentsApi is not null)
                {
                    return _docsComponentsApi;
                }

                _docsComponentsApi = new DocsComponents();
                foreach (var item in Components)
                {
                    if (item.IsNavGroup)
                    {
                        foreach (var groupComponent in item.GroupComponents)
                        {
                            _docsComponentsApi.AddItem(groupComponent.Name, groupComponent.Type);
                        }
                    }
                    else
                    {
                        _docsComponentsApi.AddItem(item.Name, item.Type);
                    }
                }

                return _docsComponentsApi;
            }
        }

        /// <summary>
        /// Gets the sub-menu, if any, matching the specified type.
        /// </summary>
        /// <param name="parent">The parent to start searching from.</param>
        /// <param name="type">The type to find.</param>
        /// <returns>The menu whose link, child type, or group component matches the type.</returns>
        public ProComponent? GetExample(DocumentedType type)
        {
            // Go through each menu...
            foreach (var menu in Components)
            {
                // Is there a menu for this type?  If so, return it.
                var component = GetExample(menu, type);
                if (component != null)
                {
                    return component;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the sub-menu, if any, matching the specified type.
        /// </summary>
        /// <param name="parent">The parent to start searching from.</param>
        /// <param name="type">The type to find.</param>
        /// <returns>The menu whose link, child type, or group component matches the type.</returns>
        public static ProComponent? GetExample(ProComponent parent, DocumentedType type)
        {
            // Does the name match the menu link?
            if (parent.ComponentName == type.NameFriendly.Replace("<TData>", "<T>"))
            {
                return parent;
            }

            // Are there child types to search?
            if (parent.ChildTypes != null)
            {
                foreach (var childType in parent.ChildTypes)
                {
                    // Does the child type's name match?
                    if (childType.Name == type.Name)
                    {
                        return parent;
                    }
                }
            }

            // Are there sub-menus to search?
            if (parent.IsNavGroup && parent.GroupComponents != null)
            {
                foreach (var subMenu in parent.GroupComponents)
                {
                    // Search one level deeper
                    var component = GetExample(subMenu, type);
                    if (component != null)
                    {
                        return component;
                    }
                }
            }

            return null;
        }
    }
}
