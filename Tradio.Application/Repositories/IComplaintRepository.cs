using FluentResults;
using Tradio.Application.Dtos.Complaints;

namespace Tradio.Application.Repositories
{
    public interface IComplaintRepository
    {
        Task<ComplaintDto?> GetComplaintAsync(int complaintId);
        Task<IEnumerable<ComplaintListItemDto>> GetComplaintsAgainstUserAsync(string userId);
        Task<IEnumerable<ComplaintListItemDto>> GetComplaintsByUserAsync(string userId);
        Task<IEnumerable<ComplaintListItemDto>> GetComplaintsWithoutReplyAsync();
    }
}