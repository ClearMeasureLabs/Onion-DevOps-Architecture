using System;
using System.Collections.Generic;
using Core.Model;

namespace ClearMeasure.OnionDevOpsArchitecture.Core.Model
{
    public class ExpenseReport
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ExpenseReportStatus Status { get; set; }
        public string Number { get; set; }

        public ExpenseReport()
        {
            Status = ExpenseReportStatus.Draft;
            Description = "";
            Title = "";
        }

        public string FriendlyStatus
        {
            get { return GetTextForStatus(); }
        }

        protected string GetTextForStatus()
        {
            return Status.ToString();
        }

        public override string ToString()
        {
            return "ExpenseReport " + Number;
        }

        protected bool Equals(ExpenseReport other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExpenseReport) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private sealed class IdEqualityComparer : IEqualityComparer<ExpenseReport>
        {
            public bool Equals(ExpenseReport x, ExpenseReport y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(ExpenseReport obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public static IEqualityComparer<ExpenseReport> IdComparer { get; } = new IdEqualityComparer();
    }
}