using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using DataAccess;
using DataAccess.Mappings;
using StructureMap;

namespace IntegrationTests
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
//                scanner.ConnectImplementationsToTypesClosing(typeof (IRequestHandler<,>));
            });
            For<IDataConfiguration>().Use<DataConfigurationStub>();

        }
    }
}