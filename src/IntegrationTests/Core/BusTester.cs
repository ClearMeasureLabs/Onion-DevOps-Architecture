using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using Core.Model;
using NUnit.Framework;
using Shouldly;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests.Core
{
    public class BusTester
    {
        [Test]
        public void ShouldResolveAndExecuteAHandler()
        {
            new DatabaseTester().Clean();
            var report = new ExpenseReport
            {
                Title = "TestExpens",
                Description = "This is an ",
                Number = "123",
                Status = ExpenseReportStatus.Cancelled
            };

            using (var context = new StubbedDataContextFactory().GetContext())
            {
                context.AddRange(report);
                context.SaveChanges();
            }

            var container = new DatabaseTester().GetContainer();
            var bus = container.GetInstance<Bus>();
            var reports = bus.Send(new ListExpenseReportsCommand());;
            reports.Length.ShouldBe(1);
        }
    }
}