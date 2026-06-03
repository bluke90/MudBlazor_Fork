using Microsoft.AspNetCore.Components;
using ProtonBlazor.Docs.Services;

namespace ProtonBlazor.Docs.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        private ProThemeProvider _proThemeProvider;

        [Inject]
        private LayoutService LayoutService { get; set; }

        protected override void OnInitialized()
        {
            LayoutService.MajorUpdateOccurred += OnMajorUpdateOccured;
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dark = await _proThemeProvider.GetSystemDarkModeAsync();

                LayoutService.UpdateDarkModeState(dark);

                await LayoutService.ApplyUserPreferencesAsync();

                await _proThemeProvider.WatchSystemDarkModeAsync(LayoutService.OnSystemModeChangedAsync);

                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            LayoutService.MajorUpdateOccurred -= OnMajorUpdateOccured;
        }

        private void OnMajorUpdateOccured(object sender, EventArgs e) => StateHasChanged();
    }
}
