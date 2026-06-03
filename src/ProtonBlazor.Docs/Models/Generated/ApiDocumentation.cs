// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.Docs.Models;

/// <summary>
/// Represents a set of XML documentation for ProtonBlazor types.
/// </summary>
public static partial class ApiDocumentation
{
    /// <summary>
    /// The generated documentation for events.
    /// </summary>
    public static Dictionary<string, DocumentedEvent> Events { get; private set; } = [];

    /// <summary>
    /// The generated documentation for fields.
    /// </summary>
    public static Dictionary<string, DocumentedField> Fields { get; private set; } = [];

    /// <summary>
    /// The generated documentation for types.
    /// </summary>
    public static Dictionary<string, DocumentedType> Types { get; private set; } = [];

    /// <summary>
    /// The generated documentation for properties.
    /// </summary>
    public static Dictionary<string, DocumentedProperty> Properties { get; private set; }

    /// <summary>
    /// The generated documentation for methods.
    /// </summary>
    public static Dictionary<string, DocumentedMethod> Methods { get; private set; } = [];

    /// <summary>
    /// Gets an event, field, method, or property by its name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DocumentedMember GetMember(string name)
    {
        // Is this an external member?
        if (!name.StartsWith("ProtonBlazor", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        // Is this an icon?  (We don't document those, but we show them as icons)
        if (name.StartsWith("ProtonBlazor.Icons", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        DocumentedMember result = GetProperty(name);
        result ??= GetField(name);
        result ??= GetEvent(name);
        result ??= GetMethod(name);
        return result;
    }

    /// <summary>
    /// Gets a documented type by its name.
    /// </summary>
    /// <param name="name">The name of the type to find.</param>
    public static DocumentedType GetType(string name)
    {
        // Anything to do?
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        // Is this an external member?
        if (name.StartsWith("System", StringComparison.OrdinalIgnoreCase) || name.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        // First, try an exact match
        if (Types.TryGetValue(name, out var match))
        {
            return match;
        }

        // Next, try with the ProtonBlazor namespace
        if (Types.TryGetValue("ProtonBlazor." + name, out match))
        {
            return match;
        }

        // Look for a component with a generic
        if (Types.TryGetValue("ProtonBlazor." + name + "`1", out match))
        {
            return match;
        }

        // Look for a component with two generics
        if (Types.TryGetValue("ProtonBlazor." + name + "`2", out match))
        {
            return match;
        }

        // Look for legacy links like "api/bar"
        if (LegacyToModernTypeNames.TryGetValue(name.ToLowerInvariant(), out var newTypeName) && Types.TryGetValue(newTypeName, out match))
        {
            return match;
        }

        // Try to match just on the name
        var looseMatch = Types.FirstOrDefault(type =>
            // Look for a match on just the name
            type.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            // Look for a match on the name with a generic
            || type.Value.Name.Equals(name + "`1", StringComparison.OrdinalIgnoreCase)
            // Look for a match on the name with two generics
            || type.Value.Name.Equals(name + "`2", StringComparison.OrdinalIgnoreCase)
            // .. or the friendly name
            || type.Value.NameFriendly.Equals(name, StringComparison.OrdinalIgnoreCase)).Value;
        if (looseMatch != null)
        {
            return looseMatch;
        }

        // Nothing found        
        return null;
    }

    /// <summary>
    /// Yields documented types whose friendly name matches the given text.
    /// </summary>
    /// <param name="text"></param>
    public static IEnumerable<DocumentedType> FindTypesByFriendlyName(string text)
    {
        return Types.Where(type => type.Value.NameFriendly.Contains(text))
            .Select(pair => pair.Value);
    }

    /// <summary>
    /// Gets a documented property by its name.
    /// </summary>
    /// <param name="name">The name of the property to find.</param>
    public static DocumentedProperty GetProperty(string name)
    {
        // Anything to do?
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        // First, try an exact match
        if (Properties.TryGetValue(name, out var match))
        {
            return match;
        }
        // Next, try with the ProtonBlazor namespace
        if (Properties.TryGetValue("ProtonBlazor." + name, out match))
        {
            return match;
        }
        // Find a match by name
        var byName = Properties.SingleOrDefault(type => type.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return byName.Value;
    }

    /// <summary>
    /// Gets a documented property by its name.
    /// </summary>
    /// <param name="name">The name of the field to find.</param>
    public static DocumentedField GetField(string name)
    {
        // Anything to do?
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        // First, try an exact match
        if (Fields.TryGetValue(name, out var match))
        {
            return match;
        }
        // Next, try with the ProtonBlazor namespace
        if (Fields.TryGetValue("ProtonBlazor." + name, out match))
        {
            return match;
        }
        // Find a match by name
        var byName = Fields.SingleOrDefault(type => type.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return byName.Value;
    }

    /// <summary>
    /// Gets a documented method by its name.
    /// </summary>
    /// <param name="name">The name of the method to find.</param>
    public static DocumentedMethod GetMethod(string name)
    {
        // Anything to do?
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        // First, try an exact match
        if (Methods.TryGetValue(name, out var match))
        {
            return match;
        }
        // Next, try with the ProtonBlazor namespace
        if (Methods.TryGetValue("ProtonBlazor." + name, out match))
        {
            return match;
        }
        // Find a match by name
        var byName = Methods.SingleOrDefault(type => type.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return byName.Value;
    }

    /// <summary>
    /// Gets a documented event by its name.
    /// </summary>
    /// <param name="name">The name of the event to find.</param>
    public static DocumentedEvent GetEvent(string name)
    {
        // Anything to do?
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        // First, try an exact match
        if (Events.TryGetValue(name, out var match))
        {
            return match;
        }
        // Next, try with the ProtonBlazor namespace
        if (Events.TryGetValue("ProtonBlazor." + name, out match))
        {
            return match;
        }
        // Find a match by name
        var byName = Events.SingleOrDefault(type => type.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return byName.Value;
    }

    /// <summary>
    /// A dictionary that maps legacy api links to the new format.
    /// </summary>
    /// <remarks>
    /// This can be removed once it is decided that users are no longer using legacy API links.
    /// </remarks>
    private static readonly Dictionary<string, string> LegacyToModernTypeNames = new()
    {
        { "alert", "ProtonBlazor.ProAlert" },
        { "appbar", "ProtonBlazor.ProAppBar" },
        { "avatar", "ProtonBlazor.ProAvatar" },
        { "avatargroup", "ProtonBlazor.ProAvatarGroup" },
        { "autocomplete", "ProtonBlazor.ProAutocomplete`1" },
        { "badge", "ProtonBlazor.ProBadge" },
        { "bar", "ProtonBlazor.Charts.Bar`1" },
        { "barchart", "ProtonBlazor.Charts.Bar`1" },
        { "breadcrumbs", "ProtonBlazor.ProBreadcrumbs" },
        { "breakpointprovider", "ProtonBlazor.ProBreakpointProvider" },
        { "button", "ProtonBlazor.ProButton" },
        { "buttonfab", "ProtonBlazor.ProFab" },
        { "buttongroup", "ProtonBlazor.ProButtonGroup" },
        { "card", "ProtonBlazor.ProCard" },
        { "cardactions", "ProtonBlazor.ProCardActions" },
        { "cardcontent", "ProtonBlazor.ProCardContent" },
        { "cardheader", "ProtonBlazor.ProCardHeader" },
        { "cardmedia", "ProtonBlazor.ProCardMedia" },
        { "carousel", "ProtonBlazor.ProCarousel`1" },
        { "carouselitem", "ProtonBlazor.ProCarouselItem" },
        { "checkbox", "ProtonBlazor.ProCheckBox`1" },
        { "chips", "ProtonBlazor.ProChip`1" },
        { "chipset", "ProtonBlazor.ProChipSet`1" },
        { "collapse", "ProtonBlazor.ProCollapse" },
        { "colorpicker", "ProtonBlazor.ProColorPicker" },
        { "container", "ProtonBlazor.ProContainer" },
        { "datagrid", "ProtonBlazor.ProDataGrid`1" },
        { "datepicker", "ProtonBlazor.ProDatePicker" },
        { "daterangepicker", "ProtonBlazor.ProDateRangePicker" },
        { "dialog", "ProtonBlazor.ProDialog" },
        { "dialoginstance", "ProtonBlazor.ProDialogContainer" },
        { "dialogprovider", "ProtonBlazor.ProDialogProvider" },
        { "divider", "ProtonBlazor.ProDivider" },
        { "donut", "ProtonBlazor.Charts.Donut`1" },
        { "donutchart", "ProtonBlazor.Charts.Donut`1" },
        { "drawer", "ProtonBlazor.ProDrawer" },
        { "drawercontainer", "ProtonBlazor.ProDrawerContainer" },
        { "drawerheader", "ProtonBlazor.ProDrawerHeader" },
        { "dynamictabs", "ProtonBlazor.ProDynamicTabs" },
        { "element", "ProtonBlazor.ProElement" },
        { "expansionpanel", "ProtonBlazor.ProExpansionPanel" },
        { "expansionpanels", "ProtonBlazor.ProExpansionPanels" },
        { "field", "ProtonBlazor.ProField" },
        { "fileuploader", "ProtonBlazor.ProFileUpload`1" },
        { "focustrap", "ProtonBlazor.ProFocusTrap" },
        { "form", "ProtonBlazor.ProForm" },
        { "grid", "ProtonBlazor.ProGrid" },
        { "hidden", "ProtonBlazor.ProHidden" },
        { "highlighter", "ProtonBlazor.ProHighlighter" },
        { "iconbutton", "ProtonBlazor.ProIconButton" },
        { "icons", "ProtonBlazor.ProIcon" },
        { "input", "ProtonBlazor.ProInput`1" },
        { "inputcontrol", "ProtonBlazor.ProInputControl" },
        { "inputlabel", "ProtonBlazor.ProInputLabel" },
        { "item", "ProtonBlazor.ProItem" },
        { "legend", "ProtonBlazor.Charts.Legend`1" },
        { "line", "ProtonBlazor.Charts.Line`1" },
        { "linechart", "ProtonBlazor.Charts.Line`1" },
        { "link", "ProtonBlazor.ProLink" },
        { "list", "ProtonBlazor.ProList`1" },
        { "listitem", "ProtonBlazor.ProListItem`1" },
        { "listsubheader", "ProtonBlazor.ProListSubheader" },
        { "maincontent", "ProtonBlazor.ProMainContent" },
        { "menu", "ProtonBlazor.ProMenu" },
        { "menuitem", "ProtonBlazor.ProMenuItem" },
        { "messagebox", "ProtonBlazor.ProMessageBox" },
        { "navgroup", "ProtonBlazor.ProNavGroup" },
        { "navlink", "ProtonBlazor.ProNavLink" },
        { "navmenu", "ProtonBlazor.ProNavMenu" },
        { "numericfield", "ProtonBlazor.ProNumericField`1" },
        { "overlay", "ProtonBlazor.ProOverlay" },
        { "pagecontentnavigation", "ProtonBlazor.ProPageContentNavigation" },
        { "pagination", "ProtonBlazor.ProPagination" },
        { "paper", "ProtonBlazor.ProPaper" },
        { "pie", "ProtonBlazor.Charts.Pie`1" },
        { "piechart", "ProtonBlazor.Charts.Pie`1" },
        { "popover", "ProtonBlazor.ProPopover" },
        { "progress", "ProtonBlazor.ProProgressLinear" },
        { "radio", "ProtonBlazor.ProRadio`1" },
        { "radiogroup", "ProtonBlazor.ProRadioGroup`1" },
        { "rangeinput", "ProtonBlazor.ProRangeInput`1" },
        { "rating", "ProtonBlazor.ProRating" },
        { "ratingitem", "ProtonBlazor.ProRatingItem" },
        { "rtlprovider", "ProtonBlazor.ProRTLProvider" },
        { "scrolltotop", "ProtonBlazor.ProScrollToTop" },
        { "select", "ProtonBlazor.ProSelect`1" },
        { "selectitem", "ProtonBlazor.ProSelectItem`1" },
        { "simpletable", "ProtonBlazor.ProSimpleTable" },
        { "skeleton", "ProtonBlazor.ProSkeleton" },
        { "slider", "ProtonBlazor.ProSlider`1" },
        { "snackbar", "ProtonBlazor.ProSnackbarProvider" },
        { "snackbarelement", "ProtonBlazor.ProSnackbarElement" },
        { "sparkline", "ProtonBlazor.Charts.Line`1" },
        { "stackedbar", "ProtonBlazor.Charts.StackedBar`1" },
        { "swipearea", "ProtonBlazor.ProSwipeArea" },
        { "switch", "ProtonBlazor.ProSwitch`1" },
        { "table", "ProtonBlazor.ProTable`1" },
        { "tablegrouprow", "ProtonBlazor.ProTableGroupRow`1" },
        { "tablepager", "ProtonBlazor.ProTablePager" },
        { "tablesortlabel", "ProtonBlazor.ProTableSortLabel`1" },
        { "tabs", "ProtonBlazor.ProTabs" },
        { "td", "ProtonBlazor.ProTd" },
        { "textfield", "ProtonBlazor.ProTextField`1" },
        { "tfootrow", "ProtonBlazor.ProTFootRow" },
        { "th", "ProtonBlazor.ProTh" },
        { "theadrow", "ProtonBlazor.ProTHeadRow" },
        { "timeline", "ProtonBlazor.ProTimeline" },
        { "timelineitem", "ProtonBlazor.ProTimelineItem" },
        { "timepicker", "ProtonBlazor.ProTimePicker" },
        { "timeseries", "ProtonBlazor.Charts.TimeSeries`1" },
        { "toggleiconbutton", "ProtonBlazor.ProToggleIconButton" },
        { "toolbar", "ProtonBlazor.ProToolBar" },
        { "tooltip", "ProtonBlazor.ProTooltip" },
        { "tr", "ProtonBlazor.ProTr" },
        { "treeview", "ProtonBlazor.ProTreeView`1" },
        { "treeviewitem", "ProtonBlazor.ProTreeViewItem`1" },
        { "treeviewitemtogglebutton", "ProtonBlazor.ProTreeViewItemToggleButton" },
        { "typography", "ProtonBlazor.Typography" },
    };
}
