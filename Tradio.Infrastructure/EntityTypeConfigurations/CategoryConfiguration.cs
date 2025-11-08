using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradio.Domain;
using Tradio.Infrastructure.Common;

namespace Tradio.Infrastructure.EntityTypeConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(DefaultCategories.Categories);
        }
    }
}
