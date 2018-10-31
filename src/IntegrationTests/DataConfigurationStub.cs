using System.Configuration;
using ClearMeasure.OnionDevOpsArchitecture.Core;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests
{
    public class DataConfigurationStub : IDataConfiguration
    {
        public string GetConnectionString()
        {
            return ConfigurationManager
                .OpenExeConfiguration(@"ClearMeasure.OnionDevOpsArchitecture.IntegrationTests.dll")
                .AppSettings.Settings["ConnectionString"].Value;
        }
    }
}