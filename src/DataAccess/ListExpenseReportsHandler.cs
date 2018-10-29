using System.Linq;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess.Mappings;

namespace ClearMeasure.OnionDevOpsArchitecture.DataAccess
{
    public class ListExpenseReportsHandler : IRequestHandler<ListExpenseReportsCommand, ExpenseReport[]>
    {
        private DataContext _context;

        public ListExpenseReportsHandler(DataContext context)
        {
            _context = context;
        }

        public ExpenseReport[] Handle(ListExpenseReportsCommand request)
        {
            ExpenseReport[] reports = _context.Set<ExpenseReport>().ToArray();
            return reports;
        }
    }
}