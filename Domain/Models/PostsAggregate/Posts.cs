using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        public string? Date { get; set; }

        public string? Photo { get; set; }

        public string? Post_txt { get; set; }

        public string? UserName_txt { get; set; }

        public int? CommentCount { get; set; }

        public int? LikeCount { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }

        [JsonIgnore]
        public ICollection<Like>? Likes { get; set; }
        

        public Posts() { }

        public Posts(int? commentCount = null, int likeCount = 0) 
        {
            CommentCount = commentCount;
            LikeCount = likeCount;
        }

        public Posts(string? post, int userId, string? photo = null, string? userName = null)
        {

            Post_txt = post ?? throw new ArgumentException(null, nameof(post));

            Photo = photo ?? throw new ArgumentException(null, nameof(photo));

            UserName_txt = userName;

            var date = DateTime.Now.ToString("dd/MM/yyyy");

            Date = date;
            UserId = userId;            
        }
    }
}
