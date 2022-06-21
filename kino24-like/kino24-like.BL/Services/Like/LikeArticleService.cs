using kino24_like.BL.Interfaces.Like;
using kino24_like.BL.Models;
using kino24_like.BL.Serialization;
using kino24_like.BL.Streaming;
using kino24_like.Core.Entities;
using kino24_like.Core.Repositories;

namespace kino24_like.BL.Services
{
    public class LikeArticleService : ILikeArticleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IStreamPublisher _streamPublisher;
        private readonly ISerializer _serializer;


        public LikeArticleService(IRepositoryWrapper repositoryWrapper, ISerializer serializer, IStreamPublisher streamPublisher) 
        {
            _repositoryWrapper = repositoryWrapper;
            _streamPublisher = streamPublisher;
            _serializer = serializer;
        }

        ///<inheritdoc/>
        public async Task AddLikeAsync(LikeArticleAdd comand, User user)
        {
            //var article = await _repositoryWrapper.Article.GetFirstOrDefaultAsync(a => a.ID.ToString() == comand.ArticleId)
            //                        ?? throw new NullReferenceException();

            var like = await _repositoryWrapper.Like.GetSingleOrDefaultAsync(a =>
                                            a.ArticleId == comand.ArticleId
                                         && a.UserId    == user.Id);
            if(like != null)
            {
                if (like.Reaction == LikeReaction.Like) return;
                like.Reaction = LikeReaction.Like;
                _repositoryWrapper.Like.Update(like);
            }
            else
            {
                like = new Like()
                    {
                        ArticleId = comand.ArticleId,
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
        public async Task RemoveLikeAsync(LikeArticleRemove comand, User user)
        {
            var like = await _repositoryWrapper.Like.GetFirstOrDefaultAsync(
                predicate: c => c.ArticleId == comand.ArticleId 
                             && c.UserId    == user.Id 
                             && c.Reaction  == LikeReaction.Like) ?? throw new NullReferenceException();

            _repositoryWrapper.Like.Delete(like);
            await _repositoryWrapper.SaveAsync();

            comand.LikeId = like.ID.ToString();
            await _streamPublisher.PublishAsync(comand, user, _serializer.Serialize(comand));
        }

        ///<inheritdoc/>
        public async Task AddDislikeAsync(DislikeArticleAdd comand, User user)
        {
            //var article = await _repositoryWrapper.Article.GetFirstOrDefaultAsync(a => a.ID.ToString() == comand.ArticleId)
            //                        ?? throw new NullReferenceException();
            var dislike = await _repositoryWrapper.Like.GetFirstOrDefaultAsync(a => 
                                                            a.ArticleId == comand.ArticleId 
                                                         && a.UserId    == user.Id);
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

                    ArticleId = comand.ArticleId,
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
        public async Task RemoveDislikeAsync(DislikeArticleRemove comand, User user)
        {
            var dislike = await _repositoryWrapper.Like.GetFirstOrDefaultAsync(
                predicate: c => c.ArticleId == comand.ArticleId
                             && c.UserId    == user.Id
                             && c.Reaction == LikeReaction.Dislike) ?? throw new NullReferenceException();

            _repositoryWrapper.Like.Delete(dislike);
            await _repositoryWrapper.SaveAsync();

            comand.LikeId = dislike.ID.ToString();
            await _streamPublisher.PublishAsync(comand, user, _serializer.Serialize(comand));
        }
    }
}
