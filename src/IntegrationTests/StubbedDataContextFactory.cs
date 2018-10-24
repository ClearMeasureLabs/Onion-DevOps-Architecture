using System;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess.Mappings;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests
{
    public class StubbedDataContextFactory : IDisposable
    {
        private readonly DataContext _context;
        private readonly DataConfigurationStub _config;

        public StubbedDataContextFactory()
        {
            _config = new DataConfigurationStub();
            _context = new DataContext(_config);
        }

        public DataConfigurationStub Config => _config;

        public DataContext GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}