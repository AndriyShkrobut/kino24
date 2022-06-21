using kino24_like.Core.Repositories.Contracts;

namespace kino24_like.Core.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private FeedbackServiceDBContext _dbContext;
        private IArticleRepository _article;
        
        private ICommentRepository _comment;

        private ILikeRepository _like;

        public IArticleRepository Article
        {
            get
            {
                if (_article == null)
                {
                    _article = new ArticleRepository(_dbContext);
                }

                return _article;
            }
        }

        public ICommentRepository Comment
        {
            get
            {
                if (_comment == null)
                {
                    _comment = new CommentRepository(_dbContext);
                }
                return _comment;
            }
        }

        public ILikeRepository Like
        {
            get
            {
                if (_like == null)
                {
                    _like = new LikeRepository(_dbContext);
                }
                return _like;
            }
        }

        public RepositoryWrapper(FeedbackServiceDBContext _FeedbackServiceDBContext)
        {
            _dbContext = _FeedbackServiceDBContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}