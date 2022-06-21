using System.ComponentModel.DataAnnotations;

namespace kino24_like.Core.Entities
{
    public class Comment 
    {
        public Guid ID { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string ArticleId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
    }
}
