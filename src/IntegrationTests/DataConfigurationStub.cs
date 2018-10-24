using DataAccess;

namespace IntegrationTests
{
    public class DataConfigurationStub : IDataConfiguration
    {
        public string GetConnectionString()
        {
            return @"server=localhost\SQL2017;database=OnionDevOpsArchitecture;Integrated Security=true;";
        }
    }
}