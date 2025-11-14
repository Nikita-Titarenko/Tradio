using FluentResults;
using Tradio.Application.Dtos.ComplaintReplies;
using Tradio.Application.Dtos.Complaints;

namespace Tradio.Application.Services.ComplaintReplies
{
    public interface IComplaintReplyService
    {
        Task<Result<ComplaintReplyDto>> CreateComplaintReplyAsync(CreateComplaintReplyDto dto, string userId);
        Task<Result<ComplaintReplyDto>> GetComplaintReplyDtoAsync(int complaintId);
    }
}