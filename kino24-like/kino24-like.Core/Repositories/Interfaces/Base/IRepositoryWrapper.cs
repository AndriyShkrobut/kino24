using kino24_like.Core.Repositories.Contracts;

namespace kino24_like.Core.Repositories
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        ICommentRepository Comment { get; }
        ILikeRepository Like { get; }

        void Save();

        Task SaveAsync();
    }
}
