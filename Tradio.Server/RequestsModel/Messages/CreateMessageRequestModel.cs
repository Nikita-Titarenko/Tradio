using Tradio.Server.Attributes;

namespace Tradio.Server.RequestsModel.Messages
{
    public class CreateMessageRequestModel
    {
        public string ReceiverId { get; set; } = string.Empty;

        public int ServiceId { get; set; }

        [StringLengthWithCode(200)]
        public string Text { get; set; } = string.Empty;
    }
}
