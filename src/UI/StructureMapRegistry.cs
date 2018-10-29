using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using StructureMap;

namespace ClearMeasure.OnionDevOpsArchitecture.UI
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ExpenseReport>();
                scanner.Assembly("ClearMeasure.OnionDevOpsArchitecture.DataAccess");
                scanner.AssemblyContainingType<Startup>();
                scanner.WithDefaultConventions();
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
            });
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(container => container.GetInstance);
            For<Bus>().Use<Bus>();
        }
    }
}