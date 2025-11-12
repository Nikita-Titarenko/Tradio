using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class Message
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int ApplicationUserServiceId { get; set; }

        public ApplicationUserService ApplicationUserService { get; set; } = default!;

        public bool IsFromProvider { get; set; }

        public bool IsRead { get; set; }
    }
}
