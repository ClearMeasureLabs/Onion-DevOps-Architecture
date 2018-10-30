using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess.Mappings;
using StructureMap;

namespace ClearMeasure.OnionDevOpsArchitecture.Core.AppStartup
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AssemblyContainingType<ExpenseReport>();
                scanner.AssemblyContainingType<DataContext>();
                scanner.Assembly("ClearMeasure.OnionDevOpsArchitecture.UI");
                scanner.WithDefaultConventions();
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
            });
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(container => container.GetInstance);
            For<Bus>().Use<Bus>();
        }
    }
}