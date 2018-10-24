using System;
using System.ComponentModel;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using Core.Model;
using DataAccess.Mappings;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using IContainer = StructureMap.IContainer;

namespace IntegrationTests.DataAccess.Mappings
{
    public class ExpenseReportMappingTester
    {
        [Test]
        public void ShouldPersist()
        {
            new DatabaseTester().Clean();
            var report = new ExpenseReport
            {
                Title = "TestExpens",
                Description = "This is an ",
                Number = "123",
                Status = ExpenseReportStatus.Cancelled
            };

            using (DataContext context = new StubbedDataContextFactory().GetContext())
            {
                context.Add(report);
                context.SaveChanges();
            }

            ExpenseReport rehydratedExpenseReport;
            using (DataContext context = new StubbedDataContextFactory().GetContext())
            {
                rehydratedExpenseReport = context.Find<ExpenseReport>(report.Id);
            }

            rehydratedExpenseReport.Title.ShouldBe(report.Title);
            rehydratedExpenseReport.Description.ShouldBe(report.Description);
            rehydratedExpenseReport.Number.ShouldBe(report.Number);
            rehydratedExpenseReport.Status.ShouldBe(report.Status);

        }
    }
}