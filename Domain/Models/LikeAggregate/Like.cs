using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Domain.Models.LikeAggregate
{
    public class Like
    {

        [Key]
        public int LikeId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public int PostId { get; set; }

        [JsonIgnore]
        public Posts? Post { get; set; }


        public Like()
        {
        }

        public Like(int userId, int postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
