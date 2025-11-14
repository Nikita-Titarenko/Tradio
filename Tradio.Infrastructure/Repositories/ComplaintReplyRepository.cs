using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class ComplaintReplyRepository : Repository<ComplaintReply>, IComplaintReplyRepository
    {
        public ComplaintReplyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        //public async Task<ComplaintReply> GetComplaintReplAsync()
        //{

        //}
    }
}
