using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearMeasure.OnionDevOpsArchitecture.DataAccess.Mappings
{
    public interface IEntityFrameworkMapping
    {
        EntityTypeBuilder Map(ModelBuilder modelBuilder);
    }
}