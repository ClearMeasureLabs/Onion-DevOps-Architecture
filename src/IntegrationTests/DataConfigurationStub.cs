using System.Configuration;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests
{
    public class DataConfigurationStub : IDataConfiguration
    {
        public string GetConnectionString()
        {
            return ConfigurationManager
                .OpenExeConfiguration(@"ClearMeasure.OnionDevOpsArchitecture.IntegrationTests.dll")
                .ConnectionStrings.ConnectionStrings["Database"]
                .ConnectionString;
//            return @"server=localhost\SQL2017;database=OnionDevOpsArchitecture;Integrated Security=true;";
        }
    }
}