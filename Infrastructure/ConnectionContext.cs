using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.NotificationAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Posts> Posts { get; set; } = default!;
        public DbSet<Comment> Comment { get; set; } = default!;
        public DbSet<PostLike> PostLike { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;

        //string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=postgresql://postgres:wY3e8oaev6DbYeDyTYDg@containers-us-west-142.railway.app:6449/railway;Database=railway;User Id=postgres;Password=wY3e8oaev6DbYeDyTYDg;");

        //"Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;"
    }
}
