using kino24_like.BL.Interfaces.Comment;
using kino24_like.BL.Streaming;
using kino24_like.Core.Entities;
using kino24_like.Core.Repositories;
using kino24_like.BL.Models;
using kino24_like.BL.Interfaces;
using kino24_like.BL.Serialization;

namespace kino24_like.BL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IStreamPublisher _streamPublisher;
        private readonly IUniqueIdService _uniqueIdService;
        private readonly ISerializer _serializer;
        public CommentService(IRepositoryWrapper repositoryWrapper, IStreamPublisher streamPublisher, IUniqueIdService uniqueIdService, ISerializer serializer) 
        {
            _repositoryWrapper = repositoryWrapper;
            _streamPublisher = streamPublisher;
            _uniqueIdService = uniqueIdService;
            _serializer = serializer;
        }

        ///<inheritdoc/>
        public async Task AddCommentAsync(CommentArticleAdd commentArticleAddComand, User user)
        {
            //var article = await _repositoryWrapper.Article.GetFirstOrDefaultAsync(a => a.ID.ToString() == itemId) 
            //                        ?? throw new NullReferenceException();
            var comment = new Comment() { 
                ArticleId = commentArticleAddComand.ArticleId, 
                UserId    = user.Id, 
                Text      = commentArticleAddComand.Text, 
                Timestamp = DateTime.Now.ToUniversalTime() };
            await _repositoryWrapper.Comment.CreateAsync(comment);
            await _repositoryWrapper.SaveAsync();

            commentArticleAddComand.CommentId = comment.ID.ToString();
            await _streamPublisher.PublishAsync(commentArticleAddComand, user, _serializer.Serialize(commentArticleAddComand));
        }

        ///<inheritdoc/>
        public async Task RemoveCommentAsync(CommentArticleRemove commentArticleRemoveComand, User user)
        {
            var comment = await _repositoryWrapper.Comment.GetFirstOrDefaultAsync(
                predicate: c => c.ID.ToString() == commentArticleRemoveComand.CommentId && c.UserId == user.Id
                //include: async source => source.Include(a => a.Like)
                ) ?? throw new NullReferenceException();

            _repositoryWrapper.Comment.Delete(comment);
            await _repositoryWrapper.SaveAsync();

            await _streamPublisher.PublishAsync(commentArticleRemoveComand, user, _serializer.Serialize(commentArticleRemoveComand));
        }
    }
}
