namespace kino24_like.BL.Models
{
    public class CommentArticleAddComand : IActionType
    {
        public string ArticleId { get; set; }
        public string Text { get; set; }
        public string GetType() => "CommentArticleAdd";
    }

    public class CommentArticleRemoveComand : IActionType
    {
        public string CommentId { get; set; }
        public string GetType() => "CommentArticleRemove";
    }

    public class LikeArticleAddComand : IActionType
    {
        public string ArticleId { get; set; }
        public string GetType() => "LikeArticleAdd";
    }

    public class LikeArticleRemoveComand : IActionType
    {
        public string ArticleId { get; set; }
        public string GetType() => "LikeArticleRemove";
    }

    public class DislikeArticleAddComand : IActionType
    {
        public string ArticleId { get; set; }
        public string GetType() => "DislikeArticleAdd";
    }

    public class DislikeArticleRemoveComand : IActionType
    {
        public string ArticleId { get; set; }
        public string GetType() => "DislikeArticleRemove";
    }

    public class LikeCommentAddComand : IActionType
    {
        public string CommentId { get; set; }
        public string GetType() => "LikeCommentAdd";
    }

    public class LikeCommentRemoveComand : IActionType
    {
        public string CommentId { get; set; }
        public string GetType() => "LikeCommentRemove";
    }

    public class DislikeCommentAddComand : IActionType
    {
        public string CommentId { get; set; }
        public string GetType() => "DislikeCommentAdd";
    }

    public class DislikeCommentRemoveComand : IActionType
    {
        public string CommentId { get; set; }
        public string GetType() => "DislikeCommentRemove";
    }

}