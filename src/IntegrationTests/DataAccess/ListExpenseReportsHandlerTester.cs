using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using ClearMeasure.OnionDevOpsArchitecture.DataAccess;
using Core.Model;
using NUnit.Framework;
using Shouldly;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests.DataAccess
{
    public class ListExpenseReportsHandlerTester
    {
        [Test]
        public void ShouldRetrieveAllExpenseReports()
        {
            new DatabaseTester().Clean();
            var report = new ExpenseReport
            {
                Title = "TestExpens",
                Description = "This is an ",
                Number = "123",
                Status = ExpenseReportStatus.Cancelled
            };
            var report2 = new ExpenseReport
            {
                Title = "TestExpens",
                Description = "This is an ",
                Number = "123",
                Status = ExpenseReportStatus.Cancelled
            };

            using (var context = new StubbedDataContextFactory().GetContext())
            {
                context.AddRange(report, report2);
                context.SaveChanges();
            }

            ListExpenseReportsHandler handler =
                new ListExpenseReportsHandler(new StubbedDataContextFactory().GetContext());
            var expenseReports = handler.Handle(new ListExpenseReportsCommand());
            expenseReports.Length.ShouldBe(2);
            expenseReports.ShouldContain(report);
            expenseReports.ShouldContain(report2);
        }
    }
}