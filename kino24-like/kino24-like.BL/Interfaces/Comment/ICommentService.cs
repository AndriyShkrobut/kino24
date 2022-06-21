using kino24_like.BL.Models;

namespace kino24_like.BL.Interfaces.Comment
{
    public interface ICommentService
    {
        /// <summary>
        /// Add comment for item
        /// </summary>
        /// <param name="commentArticleAddComand"></param>
        /// <param name="user"></param>
        /// <returns>Result of adding comment</returns>
        Task AddCommentAsync(CommentArticleAdd commentArticleAddComand, User user);

        /// <summary>
        /// Remove comment for item
        /// </summary>
        /// <param name="commentArticleRemoveComand"></param>
        /// <param name="user"></param>
        /// <returns>Result of removing comment</returns>
        Task RemoveCommentAsync(CommentArticleRemove commentArticleRemoveComand, User user);
    }
}
