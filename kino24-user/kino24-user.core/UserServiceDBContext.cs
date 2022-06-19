using kino24_user.core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kino24_user.core
{
    public class UserServiceDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public UserServiceDBContext(DbContextOptions<UserServiceDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
