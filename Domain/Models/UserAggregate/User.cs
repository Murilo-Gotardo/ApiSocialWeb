using apiSocialWeb.Domain.Models.NotificationAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string? Name_txt { get; set; }

        public string? Email { get; set; }

        public string? Photo { get; set; }

        [JsonIgnore]
        public ICollection<Posts>? Posts { get; set; }

        [JsonIgnore]
        public ICollection<Notification>? Notifications { get; set; }

        public User() { }

        public User(string? name, string? email, string? photo)
        {
            Name_txt = name ?? throw new ArgumentException(null, nameof(name));

            Email = email ?? throw new ArgumentException(null, nameof(email));

            Photo = photo ?? throw new ArgumentException(null, nameof(photo));

            //Notifications = notifications ?? throw new ArgumentException(nameof(notifications));

            //Like = like ?? throw new ArgumentException(nameof(Like));
        }
    }
}
