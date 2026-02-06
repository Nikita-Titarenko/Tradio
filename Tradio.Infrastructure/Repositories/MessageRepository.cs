using Microsoft.EntityFrameworkCore;
using Stripe;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ChatDto?> GetMessagesAsync(int applicationUserServiceId, string applicationUserId)
        {
            return await _dbContext
                .ApplicationUserServices
                .Where(us => us.Id == applicationUserServiceId)
                .Select(us => new ChatDto
                {
                    ApplicationUserId = us.ApplicationUserId != applicationUserId ? us.ApplicationUserId : us.Service.ApplicationUserId,
                    ServiceId = us.Id,
                    ServiceName = us.Service.Name,
                    FullName = _dbContext.Users.Where(u => u.Id == us.ApplicationUserId).Select(u => u.Fullname).First(),
                    ApplicationUserServiceId = applicationUserServiceId,
                    Messages = us.Messages.Select(m => new MessageDto
                    {
                        Id = m.Id,
                        ApplicationUserServiceId = m.ApplicationUserServiceId,
                        CreationDateTime = m.CreationDateTime,
                        IsYourMessage = m.IsFromProvider ? us.Service.ApplicationUserId == applicationUserId : us.ApplicationUserId == applicationUserId,
                        IsRead = m.IsRead,
                        Text = m.Text
                    })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ChatDto?> GetMessagesByServiceAsync(int serviceId, string applicationUserId)
        {
            return await _dbContext
                .Services
                .Where(s => s.Id == serviceId)
                .Select(s => new ChatDto
                {
                    ApplicationUserId = s.ApplicationUserId != applicationUserId ? s.ApplicationUserId : s.ApplicationUserId,
                    ServiceId = s.Id,
                    ServiceName = s.Name,
                    FullName = _dbContext.Users.Where(u => u.Id == s.ApplicationUserId).Select(u => u.Fullname).First(),
                    ApplicationUserServiceId = s.ApplicationUserServices
                        .Where(us => us.ServiceId == serviceId
                        && us.ApplicationUserId == applicationUserId)
                        .Select(us => us.Id)
                        .FirstOrDefault(),
                    Messages = s.ApplicationUserServices
                        .Where(us => us.ServiceId == serviceId 
                        && us.ApplicationUserId == applicationUserId)
                        .SelectMany(us => us.Messages.Select(m => new MessageDto
                        {
                            Id = m.Id,
                            ApplicationUserServiceId = m.ApplicationUserServiceId,
                            CreationDateTime = m.CreationDateTime,
                            IsYourMessage = m.IsFromProvider ? s.ApplicationUserId == applicationUserId : us.ApplicationUserId == applicationUserId,
                            IsRead = m.IsRead,
                            Text = m.Text
                        })).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
