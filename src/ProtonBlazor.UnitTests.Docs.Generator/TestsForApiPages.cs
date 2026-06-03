using System;
using System.Text.RegularExpressions;
using ProtonBlazor.Charts;

namespace ProtonBlazor.UnitTests.Docs.Generator;

public partial class TestsForApiPages
{
    /// <summary>
    /// The current production links to API documentation.
    /// </summary>
    private readonly string[] _legacyApiAddresses = [
        // available from the main menu in "API" group
        "api/alert",
        "api/appbar",
        "api/autocomplete",
        "api/avatar",
        "api/badge",
        "api/barchart",
        "api/breadcrumbs",
        "api/breakpointprovider",
        "api/button",
        "api/buttonfab",
        "api/buttongroup",
        "api/card",
        "api/carousel",
        "api/checkbox",
        "api/chips",
        "api/chipset",
        "api/colorpicker",
        "api/container",
        "api/datagrid",
        "api/datepicker",
        "api/dialog",
        "api/divider",
        "api/donutchart",
        "api/drawer",
        "api/element",
        "api/expansionpanels",
        "api/field",
        "api/fileuploader",
        "api/focustrap",
        "api/form",
        "api/grid",
        "api/hidden",
        "api/highlighter",
        "api/iconbutton",
        "api/icons",
        "api/linechart",
        "api/link",
        "api/list",
        "api/menu",
        "api/messagebox",
        "api/navmenu",
        "api/numericfield",
        "api/overlay",
        "api/pagination",
        "api/paper",
        "api/piechart",
        "api/popover",
        "api/progress",
        "api/radio",
        "api/rating",
        "api/scrolltotop",
        "api/select",
        "api/simpletable",
        "api/skeleton",
        "api/slider",
        "api/snackbar",
        "api/swipearea",
        "api/switch",
        "api/table",
        "api/tabs",
        "api/textfield",
        "api/timepicker",
        "api/timeline",
        "api/toggleiconbutton",
        "api/toolbar",
        "api/tooltip",
        "api/treeview",
        "api/typography",

        // subelements - available from components/* pages
        "api/drawerheader",
        "api/drawercontainer",
        "api/navlink",
        "api/navgroup",
        "api/item",
        "api/dynamictabs",
        "api/expansionpanel",
        "api/timelineitem",
        "api/cardactions",
        "api/cardcontent",
        "api/cardheader",
        "api/cardmedia",
        "api/treeviewitem",
        "api/treeviewitemtogglebutton",
        "api/listitem",
        "api/listsubheader",
        "api/carouselitem",
        "api/dialoginstance",
        "api/dialogprovider",
        "api/avatargroup",
        "api/menuitem",
        "api/radiogroup",
        "api/selectitem",
        "api/ratingitem",

        // API pages not linked from the documentation web site, but still available through the URL
        "api/THeadRow",
        "api/TFootRow",
        "api/Tr",
        "api/Th",
        "api/Td",
        "api/TableGroupRow",
        "api/TableSortLabel",
        "api/TablePager",
        "api/InputLabel",
        "api/InputControl",
        "api/Input",
        "api/RangeInput",
        "api/MainContent",
        "api/DateRangePicker",
        "api/Collapse",
        "api/PageContentNavigation",
        "api/RTLProvider",
        "api/SnackbarElement",
        "api/SparkLine",
    ];

    /// <summary>
    /// A list of types which have an example page ("/component/*") to link to.
    /// </summary>
    /// <remarks>
    /// This list should match the types mentioned in the <c>MenuService</c> class in ProtonBlazor.Docs.
    /// </remarks>
    public List<Type> TypesWithExamples =
    [
        typeof(ProContainer), typeof(ProGrid), typeof(ProItem), typeof(ProHidden),  typeof(ProBreakpointProvider), typeof(ProChip<object>), typeof(ProChipSet<object>),
        typeof(ProBadge), typeof(ProAppBar), typeof(ProDrawer), typeof(ProDrawerHeader), typeof(ProDrawerContainer), typeof(ProDropZone<object>), typeof(ProDropContainer<object>),
        typeof(ProDynamicDropItem<object>), typeof(ProLink), typeof(ProMenu), typeof(ProMenuItem), typeof(ProMessageBox), typeof(ProNavMenu), typeof(ProNavLink), typeof(ProNavGroup),
        typeof(ProTabs), typeof(ProTabPanel), typeof(ProDynamicTabs), typeof(ProProgressCircular), typeof(ProProgressLinear), typeof(ProDialog), typeof(ProDialogContainer),
        typeof(ProDialogProvider), typeof(SnackbarService), typeof(ProSnackbarProvider), typeof(ProSnackbarElement), typeof(ProAvatar), typeof(ProAvatarGroup),
        typeof(ProAlert), typeof(ProCard), typeof(ProCardActions), typeof(ProCardContent), typeof(ProCardHeader), typeof(ProCardMedia), typeof(ProDivider),
        typeof(ProExpansionPanels), typeof(ProExpansionPanel), typeof(ProImage), typeof(ProIcon), typeof(ProList<object>), typeof(ProListItem<object>), typeof(ProListSubheader),
        typeof(ProPaper), typeof(ProRating), typeof(ProRatingItem), typeof(ProSkeleton), typeof(ProTableBase), typeof(ProTable<object>), typeof(ProTablePager),
        typeof(ProTableGroupRow<object>), typeof(ProTableSortLabel<object>), typeof(ProTd), typeof(ProTh), typeof(ProTr), typeof(ProTFootRow), typeof(ProTHeadRow),
        typeof(ProDataGrid<object>), typeof(Column<object>), typeof(FilterHeaderCell<object>), typeof(FooterCell<object>), typeof(HeaderCell<object>), typeof(HierarchyColumn<object>), typeof(ProDataGridPager<object>),
        typeof(TemplateColumn<object>), typeof(ProSimpleTable), typeof(ProTooltip), typeof(ProText), typeof(ProOverlay), typeof(ProHighlighter), typeof(ProElement),
        typeof(ProFocusTrap), typeof(ProTreeView<object>), typeof(ProTreeViewItem<object>), typeof(ProTreeViewItemToggleButton),  typeof(ProBreadcrumbs), typeof(ProScrollToTop),
        typeof(ProPopover),  typeof(ProSwipeArea), typeof(ProToolBar), typeof(ProCarousel<object>), typeof(ProCarouselItem), typeof(ProTimeline), typeof(ProTimelineItem),
        typeof(ProPagination), typeof(ProStack), typeof(ProSpacer), typeof(ProCollapse), typeof(ProStepper), typeof(ProStep), typeof(ProRadio<object>), typeof(ProRadioGroup<object>),
        typeof(ProCheckBox<object>), typeof(ProSelect<object>), typeof(ProSelectItem<object>), typeof(ProSlider<int>), typeof(ProSwitch<object>), typeof(ProTextField<object>),
        typeof(ProNumericField<object>), typeof(ProForm), typeof(ProAutocomplete<object>), typeof(ProField), typeof(ProFileUpload<object>), typeof(ProToggleGroup<object>), typeof(ProToggleItem<object>),
        typeof(ProDatePicker), typeof(ProDateRangePicker), typeof(ProTimePicker), typeof(ProColorPicker),  typeof(ProButton),  typeof(ProButtonGroup), typeof(ProIconButton),
        typeof(ProToggleIconButton), typeof(ProFab), typeof(ChartOptions), typeof(Donut<>), typeof(Line<>), typeof(Legend<>), typeof(Pie<>), typeof(Bar<>), typeof(HeatMap<>),typeof(StackedBar<>),
        typeof(TimeSeries<>), typeof(Radar<>), typeof(Rose<>)
    ];

    /// <summary>
    /// Ensures that an API page is available for each ProtonBlazor component.
    /// </summary>
    public bool Execute()
    {
        var success = true;
        try
        {
            Directory.CreateDirectory(Paths.TestDirPath);

            var currentCode = string.Empty;
            if (File.Exists(Paths.ApiPageTestsFilePath))
            {
                currentCode = File.ReadAllText(Paths.ApiPageTestsFilePath);
            }

            var cb = new CodeBuilder();

            cb.AddHeader();
            cb.AddLine("using System.Collections.Generic;");
            cb.AddLine("using Bunit;");
            cb.AddLine("using AwesomeAssertions;");
            cb.AddLine("using Microsoft.Extensions.DependencyInjection;");
            cb.AddLine("using ProtonBlazor.Docs.Pages.Api;");
            cb.AddLine("using ProtonBlazor.Docs.Services;");
            cb.AddLine("using NUnit.Framework;");
            cb.AddLine();
            cb.AddLine("namespace ProtonBlazor.UnitTests.Docs.Generated");
            cb.AddLine("{");
            cb.IndentLevel++;
            cb.AddLine("// These tests just check all the API pages to see if they throw any exceptions");
            cb.AddLine("[System.CodeDom.Compiler.GeneratedCodeAttribute(\"ProtonBlazor.Docs.Compiler\", \"0.0.0.0\")]");
            cb.AddLine("public partial class ApiDocsTests");
            cb.AddLine("{");
            cb.IndentLevel++;

            WritePublicTypeCases(cb);
            WriteLegacyApiCases(cb);
            WritePublicTypeTest(cb);
            WriteLegacyApiTest(cb);

            cb.IndentLevel--;
            cb.AddLine("}");
            cb.IndentLevel--;
            cb.AddLine("}");

            if (currentCode != cb.ToString())
            {
                File.WriteAllText(Paths.ApiPageTestsFilePath, cb.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error generating {Paths.ApiPageTestsFilePath} : {e.Message}");
            success = false;
        }

        return success;
    }

    /// <summary>
    /// Creates the ApiCases method that yields TestCaseData for all public ProtonBlazor types.
    /// </summary>
    public void WritePublicTypeCases(CodeBuilder cb)
    {
        cb.AddLine("public static IEnumerable<TestCaseData> ApiCases()");
        cb.AddLine("{");
        cb.IndentLevel++;

        var protonBlazorAssembly = typeof(_Imports).Assembly;
        var protonBlazorComponents = protonBlazorAssembly.GetTypes()
            .Where(type =>
                // Include public types
                type.IsPublic
                // ... which aren't excluded
                && !IsExcluded(type)
                // ... which aren't interfaces
                && !type.IsInterface
                && !ImplementsConverterInterface(type)
                // ... which aren't source generators
                && !type.Name.Contains("SourceGenerator")
                // ... which aren't extension classes
                && !type.Name.Contains("Extensions")
                // ... which aren't clone strategies
                && !type.Name.Contains("SystemTextJson"))
            .ToList();

        foreach (var type in protonBlazorComponents)
        {
            // Skip ProtonBlazor.Color and ProtonBlazor.Input types
            if (type.Name == "Color" || type.Name == "Input")
            {
                continue;
            }

            var typeName = type.Name.Replace("`", "");
            var hasExampleLink = TypesWithExamples.Exists(exampleType => exampleType.Name == type.Name);
            cb.AddLine($"yield return new TestCaseData(\"{type.Name}\", {hasExampleLink.ToString().ToLowerInvariant()}).SetName(\"{typeName}_API\");");
        }

        cb.IndentLevel--;
        cb.AddLine("}");
        cb.AddLine();
    }

    /// <summary>
    /// Creates the LegacyApiCases method that yields TestCaseData for legacy API links.
    /// </summary>
    public void WriteLegacyApiCases(CodeBuilder cb)
    {
        cb.AddLine("public static IEnumerable<TestCaseData> LegacyApiCases()");
        cb.AddLine("{");
        cb.IndentLevel++;

        foreach (var url in _legacyApiAddresses)
        {
            var component = url.Replace("api/", "");
            cb.AddLine($"yield return new TestCaseData(\"{component}\").SetName(\"{component.Replace("/", "_")}_Legacy_API\");");
        }

        cb.IndentLevel--;
        cb.AddLine("}");
        cb.AddLine();
    }

    /// <summary>
    /// Creates the test method for public type API pages.
    /// </summary>
    public void WritePublicTypeTest(CodeBuilder cb)
    {
        cb.AddLine("[Test]");
        cb.AddLine("[TestCaseSource(nameof(ApiCases))]");
        cb.AddLine("public async Task ApiPage_Renders(string typeName, bool hasExampleLink)");
        cb.AddLine("{");
        cb.IndentLevel++;
        cb.AddLine(@"_navigationManager.NavigateTo($""/components/{typeName}"");");
        cb.AddLine(@"var comp = _ctx.Render<Api>(parameters => parameters.Add(x => x.TypeName, typeName));");
        cb.AddLine(@"await _ctx.Services.GetService<IRenderQueueService>().WaitUntilEmpty();");
        cb.AddLine(@"comp.Find("".pro-breadcrumbs"");");
        cb.AddLine("if (hasExampleLink)");
        cb.AddLine("{");
        cb.IndentLevel++;
        cb.AddLine(@"var exampleLink = comp.FindComponents<ProLink>().FirstOrDefault(link => link.Instance.Href != null && link.Instance.Href.StartsWith(""/component""));");
        cb.AddLine(@"exampleLink.Should().NotBeNull();");
        cb.IndentLevel--;
        cb.AddLine("}");
        cb.IndentLevel--;
        cb.AddLine("}");
        cb.AddLine();
    }

    /// <summary>
    /// Creates the test method for legacy API links.
    /// </summary>
    public void WriteLegacyApiTest(CodeBuilder cb)
    {
        cb.AddLine("[Test]");
        cb.AddLine("[TestCaseSource(nameof(LegacyApiCases))]");
        cb.AddLine("public async Task LegacyApiPage_Renders(string component)");
        cb.AddLine("{");
        cb.IndentLevel++;
        cb.AddLine(@"_navigationManager.NavigateTo($""/components/api/{component}"");");
        cb.AddLine(@"var comp = _ctx.Render<Api>(parameters => parameters.Add(x => x.TypeName, component));");
        cb.AddLine(@"await _ctx.Services.GetService<IRenderQueueService>().WaitUntilEmpty();");
        cb.AddLine(@"comp.Find("".pro-breadcrumbs"");");
        cb.IndentLevel--;
        cb.AddLine("}");
        cb.AddLine();
    }

    /// <summary>
    /// Gets whether a type is excluded from documentation.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>When <c>true</c>, the type is excluded from documentation.</returns>
    private static bool IsExcluded(Type type)
    {
        if (ExcludedTypes.Contains(type.Name))
        {
            return true;
        }
        if (type.FullName != null && ExcludedTypes.Contains(type.FullName))
        {
            return true;
        }
        if (type.FullName != null && ExcludedTypes.Any(type.FullName.StartsWith))
        {
            return true;
        }

        return false;
    }

    private static bool ImplementsConverterInterface(Type type)
    {
        if (!type.IsClass)
        {
            return false;
        }

        return type
            .GetInterfaces()
            .Any(i => i.IsGenericType
                      && i.GetGenericTypeDefinition() == typeof(IConverter<,>));
    }

    /// <summary>
    /// Any types to exclude from documentation.
    /// </summary>
    public static List<string> ExcludedTypes { get; } =
    [
        "ActivatableCallback",
        "AbstractLocalizationInterceptor",
        "CloneableCloneStrategy`1",
        "CssBuilder",
        "ProtonBlazor._Imports",
        "ProtonBlazor.CategoryAttribute",
        "ProtonBlazor.CategoryTypes",
        "ProtonBlazor.CategoryTypes+",
        "ProtonBlazor.Colors",
        "ProtonBlazor.Colors+",
        "ProtonBlazor.Icons",
        "ProtonBlazor.Icons+",
        "ProtonBlazor.LabelAttribute",
        "ProtonBlazor.Resources.LanguageResource",
        "object",
        "string"
    ];
}
