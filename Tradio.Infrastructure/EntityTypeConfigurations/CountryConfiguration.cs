using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradio.Domain;
using Tradio.Infrastructure.Common;

namespace Eventa.Infrastructure.EntityTypeConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(DefaultCountries.Countries);
        }
    }
}
