using ClearMeasure.OnionDevOpsArchitecture.Core;
using UIRazorComponents.Server;

namespace ClearMeasure.OnionDevOpsArchitecture.UIRazorComponents.Server
{
    public class DataConfiguration : IDataConfiguration
    {
        public string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
    }
}