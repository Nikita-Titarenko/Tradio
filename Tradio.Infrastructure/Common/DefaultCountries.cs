using Tradio.Domain;

namespace Tradio.Infrastructure.Common
{
    public class DefaultCountries
    {
        public static readonly Country[] Countries = new Country[]
        {
            new Country
            {
                Id = 1,
                Name = "Україна"
            },
            new Country
            {
                Id = 2,
                Name = "USA"
            }
        };
    }
}
