using Tradio.Server.Attributes;

namespace Tradio.Server.RequestsModel.Complaints
{
    public class CreateComplaintRequestModel
    {
        [StringLengthWithCode(1000, MinimumLength = 50)]
        public string Text { get; set; } = string.Empty;

        public int ApplicationUserServiceId { get; set; }
    }
}
