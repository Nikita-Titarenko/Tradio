using FluentResults;
using Microsoft.EntityFrameworkCore;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Repositories;
using Tradio.Domain;
using Tradio.Application.Dtos.ComplaintReplies;

namespace Tradio.Infrastructure.Repositories
{
    public class ComplaintRepository : Repository<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ComplaintDto?> GetComplaintAsync(int complaintId)
        {
            return await _dbSet
                .Where(c => c.Id == complaintId)
                .Select(c => new ComplaintDto
                {
                    Id = c.Id,
                    ApplicationUserServiceId = c.ApplicationUserServiceId,
                    Text = c.Text,
                    CreationDateTime = c.CreationDateTime,
                    AuthorUserId = c.ApplicationUserService.Service.ApplicationUserId,
                    TargetUserId = c.ApplicationUserService.ApplicationUserId,
                    ComplaintReplyDto = c.ComplaintReply == null ? null : new ComplaintReplyDto
                    {
                        Id = c.ComplaintReply.Id,
                        Text = c.ComplaintReply.Text,
                        CreationDateTime = c.ComplaintReply.CreationDateTime,
                        IsApproved = c.ComplaintReply.IsApproved,
                        CreditReturning = c.ComplaintReply.CreditReturning
                    },
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ComplaintListItemDto>> GetComplaintsByUserAsync(string userId)
        {
            return await _dbSet
                .Where(c => c.ApplicationUserService.Service.ApplicationUserId == userId)
                .Select(c => new ComplaintListItemDto
                {
                    Id = c.Id,
                    ApplicationUserServiceId = c.ApplicationUserServiceId,
                    ComplaintReplyDto = c.ComplaintReply == null ? null : new ComplaintReplyDto
                    {
                        Id = c.ComplaintReply.Id,
                        Text = c.ComplaintReply.Text,
                        CreationDateTime = c.ComplaintReply.CreationDateTime,
                        IsApproved = c.ComplaintReply.IsApproved,
                        CreditReturning = c.ComplaintReply.CreditReturning
                    },
                    CreationDateTime = c.CreationDateTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ComplaintListItemDto>> GetComplaintsWithoutReplyAsync()
        {
            return await _dbSet
                .Where(c => c.ComplaintReply == null)
                .Select(c => new ComplaintListItemDto
                {
                    Id = c.Id,
                    ApplicationUserServiceId = c.ApplicationUserServiceId,
                    CreationDateTime = c.CreationDateTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ComplaintListItemDto>> GetComplaintsAgainstUserAsync(string userId)
        {
            return await _dbSet
                .Where(c => c.ApplicationUserService.ApplicationUserId == userId)
                .Select(c => new ComplaintListItemDto
                {
                    Id = c.Id,
                    ApplicationUserServiceId = c.ApplicationUserServiceId,
                    ComplaintReplyDto = c.ComplaintReply == null ? null : new ComplaintReplyDto
                    {
                        Id = c.ComplaintReply.Id,
                        Text = c.ComplaintReply.Text,
                        CreationDateTime = c.ComplaintReply.CreationDateTime,
                        IsApproved = c.ComplaintReply.IsApproved,
                        CreditReturning = c.ComplaintReply.CreditReturning
                    },
                    CreationDateTime = c.CreationDateTime
                })
                .ToListAsync();
        }
    }
}
