using FluentResults;
using Tradio.Application.Dtos.Complaints;

namespace Tradio.Application.Services.Complaints
{
    public interface IComplaintService
    {
        Task<Result<ComplaintDto>> CreateComplaintAsync(CreateComplaintDto dto);
        Task<Result<ComplaintDto>> GetComplaintDtoAsync(int complaintId);
    }
}