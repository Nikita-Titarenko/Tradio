using Tradio.Application.Dtos.Messages;

namespace Tradio.Application.Dtos.ApplicationUserServices
{
    public class ChatDto
    {
        public string ApplicationUserId { get; set; } = string.Empty;
        
        public bool IsRecipient { get; set; }

        public string FullName { get; set; } = string.Empty;

        public int ServiceId { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        public int? ApplicationUserServiceId { get; set; }
        
        public int Price { get; set; }

        public IEnumerable<MessageDto> Messages { get; set; } = [];
    }
}
