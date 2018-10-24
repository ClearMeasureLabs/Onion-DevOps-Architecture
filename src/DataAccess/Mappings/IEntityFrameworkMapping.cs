using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public interface IEntityFrameworkMapping
    {
        EntityTypeBuilder Map(ModelBuilder modelBuilder);
    }
}