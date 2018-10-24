using System;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataAccess.Mappings
{
    public class ExpenseReportMap : IEntityFrameworkMapping
    {
        public EntityTypeBuilder Map(ModelBuilder modelBuilder)
        {
            var mapping = modelBuilder.Entity<ExpenseReport>();
            mapping.UsePropertyAccessMode(PropertyAccessMode.Field);
            mapping.HasKey(x => x.Id);
            mapping.Property(x => x.Id).IsRequired()
                .HasValueGenerator<SequentialGuidValueGenerator>()
                .ValueGeneratedOnAdd()
                .HasDefaultValue(Guid.Empty);
            mapping.Property(x => x.Number).IsRequired().HasMaxLength(10);
            mapping.Property(x => x.Title).HasMaxLength(200);
            mapping.Property(x => x.Description).HasMaxLength(4000);
            mapping.Property(x => x.Status).HasMaxLength(3)
                .HasConversion(status => status.Code
                    , s => ExpenseReportStatus.FromCode(s));
            
            return mapping;
        }
    }
}