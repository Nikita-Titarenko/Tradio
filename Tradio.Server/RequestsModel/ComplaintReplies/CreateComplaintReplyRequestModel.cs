using System.ComponentModel.DataAnnotations;

namespace Tradio.Server.RequestsModel.ComplaintReplies
{
    public class CreateComplaintReplyRequestModel
    {
        public class CreateComplaintRequestModel
        {
            [StringLength(1000, MinimumLength = 50)]
            public string Text { get; set; } = string.Empty;

            public bool IsApproved { get; set; }

            public int ComplaintId { get; set; }

            public int? ReturningCredit { get; set; }
        }
    }
}
