using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProtonBlazor.Examples.Data;
using ProtonBlazor.Services;

namespace ProtonBlazor.UnitTests;

public class Program
{
    public static Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddMudServices();
        builder.Services.AddSingleton<ISearchService, SearchService>();
        builder.Services.AddSingleton<IPeriodicTableService, PeriodicTableService>();

        return builder.Build().RunAsync();
    }
}
