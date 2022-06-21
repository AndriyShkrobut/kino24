using kino24_like.Core.Entities;

namespace kino24_like.Core.Repositories
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(FeedbackServiceDBContext dbContext) : base(dbContext)
        {
        }
    }
}
