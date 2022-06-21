using kino24_like.BL.Models;
using kino24_like.BL.Models.ActionType;

namespace kino24_like.BL.Interfaces.Like
{
    public interface ILikeArticleService
    {
        /// <summary>
        /// Add like for item
        /// </summary>
        /// <param name="comand"></param>
        /// <param name="user"></param>
        /// <returns>Result of adding like</returns>
        Task AddLikeAsync(LikeArticleAdd comand, User user);

        /// <summary>
        /// Remove like for item
        /// </summary>
        /// <param name="comand"></param>
        /// <param name="user"></param>
        /// <returns>Result of removing like</returns>
        Task RemoveLikeAsync(LikeArticleRemove comand, User user);

        /// <summary>
        /// Add dislike for item
        /// </summary>
        /// <param name="comand"></param>
        /// <param name="user"></param>
        /// <returns>Result of adding dislike</returns>
        Task AddDislikeAsync(DislikeArticleAdd comand, User user);

        /// <summary>
        /// Remove dislike for item
        /// </summary>
        /// <param name="comand"></param>
        /// <param name="user"></param>
        /// <returns>Result of removing dislike</returns>
        Task RemoveDislikeAsync(DislikeArticleRemove comand, User user);
    }
}
