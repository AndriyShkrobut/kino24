using kino24_like.Core.Entities;
using kino24_like.Core.Repositories.Contracts;

namespace kino24_like.Core.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(FeedbackServiceDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
