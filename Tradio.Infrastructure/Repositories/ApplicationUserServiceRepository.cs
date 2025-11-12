using FluentResults;
using Microsoft.EntityFrameworkCore;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class ApplicationUserServiceRepository : Repository<ApplicationUserService>, IApplicationUserServiceRepository
    {
        public ApplicationUserServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ApplicationUserService?> GetApplicationUserServiceAsync(string userId, int serviceId)
        {
            return await _dbSet.Where(us => us.ApplicationUserId == userId && us.ServiceId == serviceId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ChatListItemDto>> GetProvidedServiceChatsAsync(string userId)
        {
            return await _dbSet
                .Where(us => us.Service.ApplicationUserId == userId)
                .Select(us => new
                {
                    us.Id,
                    FullName = _dbContext.Users.Where(u => u.Id == userId).Select(u => u.Fullname).First(),
                    LastMessage = us.Messages.OrderByDescending(m => m.CreationDateTime).First()
                })
                .Select(us => new ChatListItemDto
                {
                    Id = us.Id,
                    FullName = us.FullName,
                    LastMessageText = us.LastMessage.Text,
                    LastMessageDateTime = us.LastMessage.CreationDateTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatListItemDto>> GetReceivedServiceChatsAsync(string userId)
        {
            return await _dbSet
                .Where(us => us.ApplicationUserId == userId)
                .Select(us => new
                {
                    us.Id,
                    FullName = _dbContext.Users.Where(u => u.Id == userId).Select(u => u.Fullname).First(),
                    LastMessage = us.Messages.OrderByDescending(m => m.CreationDateTime).First()
                })
                .Select(us => new ChatListItemDto
                {
                    Id = us.Id,
                    FullName = us.FullName,
                    LastMessageText = us.LastMessage.Text,
                    LastMessageDateTime = us.LastMessage.CreationDateTime
                })
                .ToListAsync();
        }
    }
}
