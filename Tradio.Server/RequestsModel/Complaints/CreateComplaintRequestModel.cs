using System.ComponentModel.DataAnnotations;

namespace Tradio.Server.RequestsModel.Complaints
{
    public class CreateComplaintRequestModel
    {
        [StringLength(1000, MinimumLength = 50)]
        public string Text { get; set; } = string.Empty;

        public int ApplicationUserServiceId { get; set; }
    }
}
