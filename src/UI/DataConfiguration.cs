using System.Configuration;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess;
using Microsoft.IdentityModel.Protocols;

namespace ClearMeasure.OnionDevOpsArchitecture.UI
{
    public class DataConfiguration : IDataConfiguration
    {
        public string GetConnectionString()
        {
            return @"server=localhost\SQL2017;database=OnionDevOpsArchitecture;Integrated Security=true;";
        }
    }
}