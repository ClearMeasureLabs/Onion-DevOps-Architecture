using System;
using System.Configuration;
using DataAccess;
using DataAccess.Mappings;

namespace IntegrationTests
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

        public DataConfigurationStub Config
        {
            get { return _config; }
        }

        public DataContext GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
    }
}