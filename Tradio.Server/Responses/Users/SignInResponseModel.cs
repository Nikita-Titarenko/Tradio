namespace Tradio.Server.Responses.Users
{
    public class SignInResponseModel
    {
        public string? UserId { get; set; }

        public string? JwtToken { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
