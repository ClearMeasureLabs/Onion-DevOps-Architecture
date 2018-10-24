using IntegrationTests.DataAccess;
using StructureMap;

namespace IntegrationTests
{
    public class DatabaseTester
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new object();
        internal static IContainer Container = null;

        public void Clean()
        {
            EnsureDependenciesRegistered();
            new DatabaseEmptier().DeleteAllData();
        }

        public static IContainer EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
            {
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        Initialize();
                        _dependenciesRegistered = true;
                    }
                }
            }

            return Container;
        }

        private static void Initialize()
        {
            var container = new Container(new StructureMapRegistry());
            Container = container;;
        }
    }
}