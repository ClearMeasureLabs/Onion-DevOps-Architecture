using ClearMeasure.OnionDevOpsArchitecture.UI;
using StructureMap;

namespace ClearMeasure.OnionDevOpsArchitecture.AppStartup
{
    public class ContainerInitializer
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new object();
        private static IContainer _container;

        private static void EnsureDependenciesRegistered()
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
        }

        private static void Initialize()
        {
            var container = new Container(new StructureMapRegistry());
            _container = container;
        }

        public IContainer GetContainer()
        {
            EnsureDependenciesRegistered();
            return _container;
        }
    }
}