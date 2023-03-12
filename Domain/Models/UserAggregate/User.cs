using apiSocialWeb.Domain.Models.PostsAggregate;
using System.ComponentModel.DataAnnotations;

namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Photo { get; set; }

        //public string[]? Notifications { get; set; }

        //public string[]? Like { get; set; }

        public ICollection<Posts> Posts { get; set; }

        public User(string name, string email, string photo)
        {
            Name = name ?? throw new ArgumentException(nameof(name));

            Email = email ?? throw new ArgumentException(nameof(email));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            //Notifications = notifications ?? throw new ArgumentException(nameof(notifications)); ;

            //Like = like ?? throw new ArgumentException(nameof(Like));
        }
    }
}
