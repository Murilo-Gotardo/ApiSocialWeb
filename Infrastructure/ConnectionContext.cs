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
        public DbSet<Like> Like { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;

        //readonly string connectionString = "Server=" + Environment.GetEnvironmentVariable("PGHOST") + 
        //    ";Port=" + Environment.GetEnvironmentVariable("PGPORT")
        //    + ";Database=" + Environment.GetEnvironmentVariable("PGDATABASE")
        //    + ";User Id=" + Environment.GetEnvironmentVariable("PGUSER")
        //    + ";Password=" + Environment.GetEnvironmentVariable("PGPASSWORD");
        //readonly string connectionString2 = "Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=containers-us-west-34.railway.app;Port=7093;Database=railway;Username=postgres;Password=keQQ1Yh79Xwgq92yDmeu;");

        //"Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;"
    }
}
