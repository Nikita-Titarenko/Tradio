using System.ComponentModel.DataAnnotations;

namespace Tradio.Server.RequestsModel.Messages
{
    public class CreateMessageRequestModel
    {
        public string ReceiverId { get; set; } = string.Empty;

        public int ServiceId { get; set; }

        [StringLength(200)]
        public string Text { get; set; } = string.Empty;
    }
}
