using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Domain.Models.NotificationAggregate
{
    public class Notification
    {

        [Key]
        public int NotificationId { get; set; }

        public string? NotificationType { get; set; }

        public string? Status { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public int? PostId { get; set; }

        [JsonIgnore]
        public Posts? Post { get; set; }


        public Notification()
        {
        }

        public Notification(string? notificationType, string? status, int postId, int userId)
        {
            NotificationType = notificationType ?? throw new ArgumentException(null, nameof(notificationType));
            Status = status ?? throw new ArgumentException(null, nameof(status));

            UserId = userId;
            PostId = postId;
        }
    }
}
