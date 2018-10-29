using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess.Mappings;
using StructureMap;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ExpenseReport>();
                scanner.AssemblyContainingType<DataContext>();
                scanner.WithDefaultConventions();
                scanner.ConnectImplementationsToTypesClosing(typeof (IRequestHandler<,>));
            });
            For<IDataConfiguration>().Use<DataConfigurationStub>();
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(container => t => container.GetInstance(t));
        }
    }
}