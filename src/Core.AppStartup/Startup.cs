using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace ClearMeasure.OnionDevOpsArchitecture.Core.AppStartup
{
    public class Startup
    {
        public IServiceProvider Start(IServiceCollection services)
        {
            var container = new ContainerInitializer().GetContainer();
            container.Populate(services);
            foreach (var task in container.GetAllInstances<IStartupTask>())
            {
                task.Run();
            }
            
            return container.GetInstance<IServiceProvider>();
        }
    }
}