using System.ComponentModel.DataAnnotations;

namespace kino24_like.Core.Entities
{
    public class Like 
    {
        public Guid ID { get; set; }

        public string UserId { get; set; }

        public LikeReaction Reaction { get; set; }

        public string? ArticleId { get; set; }

        public string? CommentId { get; set; }
    }
}
