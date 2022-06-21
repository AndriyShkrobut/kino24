using kino24_like.BL.Models.ActionType;

namespace kino24_like.BL.Models
{
    public class CommentArticleAdd : IActionType
    {
        public string CommentId { get; set;}
        public string ArticleId { get; set; }
        public string Text { get; set; }
        public string GetType() => "CommentArticleAdded";
    }

    public class CommentArticleRemove : IActionType
    {
        public string CommentId { get; set; }
        public string GetType() => "CommentArticleRemoved";
    }

    public class LikeArticleAdd : ILikeReaction
    {
        public string ArticleId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "LikeArticleAdded";
    }

    public class LikeArticleRemove : ILikeReaction
    {
        public string ArticleId { get; set; }
        public string LikeId { get; set; }

        public string GetType() => "LikeArticleRemoved";

    }

    public class DislikeArticleAdd : ILikeReaction
    {
        public string ArticleId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "DislikeArticleAdded";

    }

    public class DislikeArticleRemove : ILikeReaction
    {
        public string ArticleId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "DislikeArticleRemoved";

    }

    public class LikeCommentAdd : ILikeReaction
    {
        public string CommentId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "LikeCommentAdded";
    }

    public class LikeCommentRemove : ILikeReaction
    {
        public string CommentId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "LikeCommentRemoved";
    }

    public class DislikeCommentAdd : ILikeReaction
    {
        public string CommentId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "DislikeCommentAdded";
    }

    public class DislikeCommentRemove : ILikeReaction
    {
        public string CommentId { get; set; }
        public string LikeId { get; set; }
        public string GetType() => "DislikeCommentRemoved";
    }

}
