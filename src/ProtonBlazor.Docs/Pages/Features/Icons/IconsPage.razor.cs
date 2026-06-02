// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Microsoft.AspNetCore.Components;
using ProtonBlazor.Docs.Models;
using ProtonBlazor.Interop;
using ProtonBlazor.Services;

namespace ProtonBlazor.Docs.Pages.Features.Icons
{
#nullable enable
    public partial class IconsPage : ComponentBase, IAsyncDisposable
    {
        private int _cardsPerRow;
        private bool _iconDrawerOpen;
        private ElementReference _killZone;
        private List<ProIcons> _displayedIcons = new();
        private const double IconCardWidth = 136.88; // single icon card width including margins
        private const float IconCardHeight = 144; // single icon card height including margins
        private IResizeObserver? _resizeObserver;

        [Inject]
        protected IResizeObserverFactory ResizeObserverFactory { get; set; } = null!;

        [Inject]
        protected IJsApiService JsApiService { get; set; } = null!;

        private List<ProIcons> CustomAll { get; } = new();

        private List<ProIcons> CustomBrands { get; } = new();

        private List<ProIcons> CustomFileFormats { get; } = new();

        private List<ProIcons> CustomUncategorized { get; } = new();

        private List<ProIcons> MaterialFilled { get; set; } = new();

        private List<ProIcons> MaterialOutlined { get; set; } = new();

        private List<ProIcons> MaterialRounded { get; set; } = new();

        private List<ProIcons> MaterialSharp { get; set; } = new();

        private List<ProIcons> MaterialTwoTone { get; set; } = new();

        private ProIcons SelectedIcon { get; set; } = ProIcons.Empty;

        private Size PreviewIconSize { get; set; } = Size.Medium;

        private Color PreviewIconColor { get; set; } = Color.Dark;

        private IconOrigin SelectedIconOrigin { get; set; } = IconOrigin.Material;

        private string IconCodeOutput { get; set; } = string.Empty;

        private string SearchText { get; set; } = string.Empty;

        private List<ProVirtualizedIcons> SelectedIcons => string.IsNullOrWhiteSpace(SearchText)
            ? GetVirtualizedIcons(_displayedIcons)
            : GetVirtualizedIcons(_displayedIcons.Where(mudIcon => mudIcon.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList());

        private List<ProVirtualizedIcons> GetVirtualizedIcons(List<ProIcons> iconList)
        {
            if (_cardsPerRow <= 0)
            {
                return new List<ProVirtualizedIcons>();
            }

            return iconList.Chunk(_cardsPerRow).Select(row => new ProVirtualizedIcons(row)).ToList();
        }

        private readonly IconStorage _iconTypes = new()
        {
            { IconType.Filled, typeof(ProtonBlazor.Icons.Material.Filled) },
            { IconType.Outlined, typeof(ProtonBlazor.Icons.Material.Outlined) },
            { IconType.Rounded, typeof(ProtonBlazor.Icons.Material.Rounded) },
            { IconType.Sharp, typeof(ProtonBlazor.Icons.Material.Sharp) },
            { IconType.TwoTone, typeof(ProtonBlazor.Icons.Material.TwoTone) },
            { IconType.Brands, typeof(ProtonBlazor.Icons.Custom.Brands) },
            { IconType.FileFormats, typeof(ProtonBlazor.Icons.Custom.FileFormats) },
            { IconType.Uncategorized, typeof(ProtonBlazor.Icons.Custom.Uncategorized) }
        };

        protected override async Task OnInitializedAsync()
        {
            _displayedIcons = MaterialFilled = await LoadMaterialIcons(IconType.Filled);
            MaterialOutlined = await LoadMaterialIcons(IconType.Outlined);
            MaterialRounded = await LoadMaterialIcons(IconType.Rounded);
            MaterialSharp = await LoadMaterialIcons(IconType.Sharp);
            MaterialTwoTone = await LoadMaterialIcons(IconType.TwoTone);

            await LoadCustomIcons();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _resizeObserver ??= ResizeObserverFactory.Create();
                await _resizeObserver.Observe(_killZone);

                _resizeObserver.OnResized += OnResized;

                SetCardsPerRow();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async ValueTask DisposeAsync()
        {
            if (_resizeObserver is not null)
            {
                _resizeObserver.OnResized -= OnResized;
                await _resizeObserver.DisposeAsync();
            }
        }

        private async void OnResized(IDictionary<ElementReference, BoundingClientRect> changes)
        {
            SetCardsPerRow();
            await InvokeAsync(StateHasChanged);
        }

        private void SetCardsPerRow()
        {
            if (_resizeObserver is null)
            {
                return;
            }

            _cardsPerRow = Convert.ToInt32(_resizeObserver.GetWidth(_killZone) / IconCardWidth);
        }

        private async Task<List<ProIcons>> LoadMaterialIcons(string type)
        {
            var iconType = _iconTypes[type];
            var result = GetMudIconsByTypeCategory(iconType, type);

            await Task.WhenAll();

            return result;
        }

        private async Task LoadCustomIcons()
        {
            CustomBrands.AddRange(GetMudIconsByTypeCategory(typeof(ProtonBlazor.Icons.Custom.Brands), IconType.Brands));
            CustomAll.AddRange(CustomBrands);

            CustomFileFormats.AddRange(GetMudIconsByTypeCategory(typeof(ProtonBlazor.Icons.Custom.FileFormats), IconType.FileFormats));
            CustomAll.AddRange(CustomFileFormats);

            CustomUncategorized.AddRange(GetMudIconsByTypeCategory(typeof(ProtonBlazor.Icons.Custom.Uncategorized), IconType.Uncategorized));
            CustomAll.AddRange(CustomUncategorized);

            await Task.WhenAll();
        }

        private List<ProIcons> GetMudIconsByTypeCategory(Type iconType, string category)
        {
            return iconType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Select(prop => new ProIcons(prop.Name, GetIconCodeOrDefault(prop), category))
                .ToList();
        }

        private string GetIconCodeOrDefault(FieldInfo fieldInfo) => fieldInfo.GetRawConstantValue()?.ToString() ?? string.Empty;

        private void ChangeIconCategory(string type)
        {
            _displayedIcons = type switch
            {
                IconType.Filled => MaterialFilled,
                IconType.Outlined => MaterialOutlined,
                IconType.Rounded => MaterialRounded,
                IconType.Sharp => MaterialSharp,
                IconType.TwoTone => MaterialTwoTone,
                IconType.All => CustomAll,
                IconType.Brands => CustomBrands,
                IconType.FileFormats => CustomFileFormats,
                IconType.Uncategorized => CustomUncategorized,
                _ => _displayedIcons
            };
        }

        private void OnSelectedValue(IconOrigin origin)
        {
            switch (origin)
            {
                case IconOrigin.Material:
                    ChangeIconCategory(IconType.Filled);
                    break;
                case IconOrigin.Custom:
                    ChangeIconCategory(IconType.All);
                    break;
            }

            SelectedIconOrigin = origin;
        }

        private void SetIconDrawer(ProIcons icon)
        {
            _iconDrawerOpen = true;
            SelectedIcon = new ProIcons(icon.Name, icon.Code, icon.Category);
            IconCodeOutput = $"@Icons{(SelectedIconOrigin == IconOrigin.Material ? ".Material" : ".Custom")}.{icon.Category}.{icon.Name}";
        }

        private void CloseIconDrawer() => _iconDrawerOpen = false;

        private async Task CopyTextToClipboard() => await JsApiService.CopyToClipboardAsync(IconCodeOutput);

        private string GetKillZoneStyle() => "height:65vh;width:100%;position:sticky;top:0px;";

        private struct IconType
        {
            public const string Filled = "Filled";
            public const string Outlined = "Outlined";
            public const string Rounded = "Rounded";
            public const string Sharp = "Sharp";
            public const string TwoTone = "TwoTone";
            public const string All = "All";
            public const string Brands = "Brands";
            public const string FileFormats = "FileFormats";
            public const string Uncategorized = "Uncategorized";
        }
        private enum IconOrigin
        {
            Custom,
            Material
        }
    }
}
