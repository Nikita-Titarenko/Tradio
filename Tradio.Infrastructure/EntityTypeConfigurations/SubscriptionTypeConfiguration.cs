using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradio.Domain;
using Tradio.Infrastructure.Common;

namespace Tradio.Infrastructure.EntityTypeConfigurations
{
    public class SubscriptionTypeConfiguration : IEntityTypeConfiguration<SubscriptionType>
    {
        public void Configure(EntityTypeBuilder<SubscriptionType> builder)
        {
            builder.HasData(DefaultSubscriptionTypes.SubscriptionTypes);
        }
    }
}
