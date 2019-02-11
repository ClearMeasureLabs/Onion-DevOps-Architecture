using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using Microsoft.Extensions.Configuration;

namespace UIRazorComponents.Server
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorComponents<App.Startup>();
            IServiceProvider provider = new ClearMeasure.OnionDevOpsArchitecture.Core.AppStartup.Startup().Start(services);
            services.AddTransient<Bus>(p => provider.GetService<Bus>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRazorComponents<App.Startup>();

            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appSettings.json").Build();
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
    }
}
