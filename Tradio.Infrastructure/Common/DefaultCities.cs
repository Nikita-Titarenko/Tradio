using Tradio.Domain;

namespace Tradio.Infrastructure.Common
{
    public class DefaultCities
    {
        public static readonly City[] Cities = new City[]
        {

            new City
            {
                Id = 1,
                Name = "Київ",
                CountryId = DefaultCountries.Countries[0].Id
            },
            new City
            {
                Id = 2,
                Name = "Львів",
                CountryId = DefaultCountries.Countries[0].Id
            },
            new City
            {
                Id = 3,
                Name = "Одеса",
                CountryId = DefaultCountries.Countries[0].Id
            },
            new City
            {
                Id = 4,
                Name = "Харків",
                CountryId = DefaultCountries.Countries[0].Id
            },
            new City
            {
                Id = 5,
                Name = "Дніпро",
                CountryId = DefaultCountries.Countries[0].Id
            },
            new City
            {
                Id = 6,
                Name = "Запоріжжя",
                CountryId = DefaultCountries.Countries[0].Id
            },


            new City
            {
                Id = 7,
                Name = "New York",
                CountryId = DefaultCountries.Countries[1].Id
            },
            new City
            {
                Id = 8,
                Name = "Washington",
                CountryId = DefaultCountries.Countries[1].Id
            },
            new City
            {
                Id = 9,
                Name = "Los Angeles",
                CountryId = DefaultCountries.Countries[1].Id
            },
            new City
            {
                Id = 10,
                Name = "Chicago",
                CountryId = DefaultCountries.Countries[1].Id
            },
            new City
            {
                Id = 11,
                Name = "Miami",
                CountryId = DefaultCountries.Countries[1].Id
            }
        };
    }
}
