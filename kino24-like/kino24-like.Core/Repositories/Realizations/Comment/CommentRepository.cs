using kino24_like.Core.Entities;
using kino24_like.Core.Repositories.Contracts;

namespace kino24_like.Core.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(FeedbackServiceDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
