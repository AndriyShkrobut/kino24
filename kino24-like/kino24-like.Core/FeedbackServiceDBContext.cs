using kino24_like.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace kino24_like.Core
{
    public class FeedbackServiceDBContext: DbContext
    {
        public FeedbackServiceDBContext(DbContextOptions<FeedbackServiceDBContext> options) : base(options)
        {

        }

        //public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
