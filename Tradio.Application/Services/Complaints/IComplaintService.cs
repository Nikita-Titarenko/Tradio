using FluentResults;
using Tradio.Application.Dtos.Complaints;

namespace Tradio.Application.Services.Complaints
{
    public interface IComplaintService
    {
        Task<Result<ComplaintDto>> CreateComplaintAsync(CreateComplaintDto dto, string userId);
        Task<Result<ComplaintDto>> GetComplaintDtoAsync(int complaintId);
        Task<Result<IEnumerable<ComplaintListItemDto>>> GetComplaintsAgainstUserAsync(string userId);
        Task<Result<IEnumerable<ComplaintListItemDto>>> GetComplaintsByUserAsync(string userId);
        Task<Result<IEnumerable<ComplaintListItemDto>>> GetComplaintsWithoutReplyAsync(string userId);
    }
}