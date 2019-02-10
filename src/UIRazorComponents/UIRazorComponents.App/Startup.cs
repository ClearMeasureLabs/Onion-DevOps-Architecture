using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using UIRazorComponents.App.Services;
using ClearMeasure.OnionDevOpsArchitecture.Core;

namespace UIRazorComponents.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Example of a data service
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<Bus>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
