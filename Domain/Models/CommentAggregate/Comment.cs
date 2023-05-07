using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Domain.Models.CommentAggregate
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string? Icomment_txt { get; set; }

        public string? UserName_txt { get; set; }

        public string? Photo { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int PostId { get; set; }

        [JsonIgnore]
        public Posts? Post { get; set; }


        public Comment()
        {
        }

        public Comment (string? iComment, string? photo, int postId, int userId, string? userName = null)
        {
            Icomment_txt = iComment ?? throw new ArgumentException(null, nameof(iComment));
            UserName_txt = userName ?? throw new ArgumentException(null, nameof(userName));
            Photo = photo ?? throw new ArgumentException(null, nameof(photo));
            UserId = userId;
            PostId = postId;
        }
    }
}
