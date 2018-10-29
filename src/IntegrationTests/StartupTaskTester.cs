using System;
using System.Linq;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using NUnit.Framework;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests
{
    [TestFixture]
    public class StartupTaskTester
    {
        [Test, Explicit]
        public void ShouldCreateViaReflection()
        {
            var loadedAssembly = AppDomain.CurrentDomain.Load("ClearMeasure.OnionDevOpsArchitecture.Core.DependencyResolution");
            var startupTasks = loadedAssembly.GetExportedTypes()
                .Where(t => t.IsPublic
                            && !t.IsAbstract
                            && typeof(IStartupTask).IsAssignableFrom(t));
            foreach (var startupTask in startupTasks)
            {
                var instance = (IStartupTask) Activator.CreateInstance(startupTask);
                instance.Run();
            }
        }
    }
}