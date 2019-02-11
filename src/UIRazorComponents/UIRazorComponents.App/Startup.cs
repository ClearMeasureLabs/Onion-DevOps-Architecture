using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using System.Diagnostics;

namespace UIRazorComponents.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Example of a data service
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            ExpenseReport[] reports = app.Services.GetService<Bus>().Send(new ListExpenseReportsCommand());
            Debug.WriteLine(reports[0].Title);
        }
    }
}
