using System.ComponentModel.DataAnnotations;
using Tradio.Server.Attributes;

namespace Tradio.Server.RequestsModel.Users
{
    public class RegisterUserRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLengthWithCode(254, MinimumLength = 5)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLengthWithCode(128, MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "Password must have at least one lowercase and one upperrcase letter")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [StringLengthWithCode(32, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        public int CityId { get; set; }
    }
}
