using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.NotificationAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Glimpse;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace apiSocialWeb.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Posts> Posts { get; set; } = default!;
        public DbSet<Comment> Comment { get; set; } = default!;
        public DbSet<Like> Like { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

                string filePath = Path.GetFullPath("appsettings.json");

                string file = File.ReadAllText(filePath);

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };

                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(file, settings);

                string connectionString = jsonObject.ConnectionString.Connection;
            try
            {

                if (connectionString != null)
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
                else
                {
                    throw new Exception("Database connection string environment variable not set.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(connectionString, ex);
            }

            
        }

        //"Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;"

        //Server=containers-us-west-138.railway.app;Port=7774;Database=railway;Username=postgres;Password=ElRrXNyok6t7i1Ki7kJy;

        public ConnectionContext() { }
    }
}
