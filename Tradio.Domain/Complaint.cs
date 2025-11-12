using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class Complaint
    {
        public int Id { get; set; }

        [MaxLength(1000)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int ApplicationUserServiceId { get; set; }

        public ApplicationUserService ApplicationUserService { get; set; } = default!;
    }
}