using kino24_like.BL.Interfaces.Like;
using kino24_like.BL.Models;
using kino24_like.BL.Serialization;
using kino24_like.BL.Streaming;
using kino24_like.Core.Entities;
using kino24_like.Core.Repositories;

namespace kino24_like.BL.Services
{
    public class LikeCommentService : ILikeCommentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IStreamPublisher _streamPublisher;
        private readonly ISerializer _serializer;
        public LikeCommentService(IRepositoryWrapper repositoryWrapper, IStreamPublisher streamPublisher, ISerializer serializer)
        {
            _repositoryWrapper = repositoryWrapper;
            _streamPublisher = streamPublisher;
            _serializer = serializer;
        }

        ///<inheritdoc/>
        public async Task AddLikeAsync(LikeCommentAdd comand, User user)
        {
            //var article = await _repositoryWrapper.Comment.GetFirstOrDefaultAsync(a => a.ID.ToString() == comand.CommentId)
            //                        ?? throw new NullReferenceException();

            var like = await _repositoryWrapper.Like.GetSingleOrDefaultAsync(a => a.CommentId == comand.CommentId && a.UserId == user.Id);
            if (like != null)
            {
                if (like.Reaction == LikeReaction.Like) return;
                like.Reaction = LikeReaction.Like;
                _repositoryWrapper.Like.Update(like);
            }
            else
            {
                like = new Like()
                {
                    CommentId = comand.CommentId,
                    UserId = user.Id,
                    Reaction = LikeReaction.Like
                };
                await _repositoryWrapper.Like.CreateAsync(like);
            }
            await _repositoryWrapper.SaveAsync();

            comand.LikeId = like.ID.ToString();
            await _streamPublisher.PublishAsync(comand, user, _serializer.Serialize(comand));
        }

        ///<inheritdoc/>
        public async Task RemoveLikeAsync(LikeCommentRemove comand, User user)
        {
            var like = await _repositoryWrapper.Like.GetFirstOrDefaultAsync(
                predicate: c =>  c.CommentId == comand.CommentId
                             && c.UserId == user.Id
                             && c.Reaction == LikeReaction.Like) ?? throw new NullReferenceException();

            _repositoryWrapper.Like.Delete(like);
            await _repositoryWrapper.SaveAsync();

            comand.LikeId = like.ID.ToString();
            await _streamPublisher.PublishAsync(comand, user, _serializer.Serialize(comand));
        }

        ///<inheritdoc/>
        public async Task AddDislikeAsync(DislikeCommentAdd comand, User user)
        {
            //var article = await _repositoryWrapper.Comment.GetFirstOrDefaultAsync(a => a.ID.ToString() == comand.CommentId)
            //                        ?? throw new NullReferenceException();
            var dislike = await _repositoryWrapper.Like.GetFirstOrDefaultAsync(a =>
                                                            a.CommentId == comand.CommentId
                                                         && a.UserId == user.Id);
            if (dislike != null)
            {
                if (dislike.Reaction == LikeReaction.Dislike) return;
                dislike.Reaction = LikeReaction.Dislike;
                _repositoryWrapper.Like.Update(dislike);
            }
            else
            {
                dislike = new Like()
                {
                    CommentId = comand.CommentId,
                    UserId = user.Id,
                    Reaction = LikeReaction.Dislike
                };
                await _repositoryWrapper.Like.CreateAsync(dislike);
            }
            await _repositoryWrapper.SaveAsync();

            comand.LikeId = dislike.ID.ToString();
            await _streamPublisher.PublishAsync(comand, user, _serializer.Serialize(comand));
        }

        ///<inheritdoc/>
        public async Task RemoveDislikeAsync(DislikeCommentRemove comand, User user)
        {
            var dislike = await _repositoryWrapper.Like.GetFirstOrDefaultAsync(
                predicate: c => c.CommentId == comand.CommentId
                             && c.UserId == user.Id
                             && c.Reaction == LikeReaction.Dislike) ?? throw new NullReferenceException();

            _repositoryWrapper.Like.Delete(dislike);
            await _repositoryWrapper.SaveAsync();

            comand.LikeId = dislike.ID.ToString();
            await _streamPublisher.PublishAsync(comand, user, _serializer.Serialize(comand));
        }
    }
}
