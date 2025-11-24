namespace Tradio.Application.Dtos.Users
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;

        public int CreditCount { get; set; }

        public string CityName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;
    }
}
