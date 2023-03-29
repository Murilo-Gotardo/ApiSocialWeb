using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        public string? Date { get; set; }

        public string? Photo { get; set; }

        public string? Post { get; set; }

        public string? UserName { get; set; }

        public int? CommentCount { get; set; }

        public int? LikeCount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }
        


        public Posts(int commentCount = 0, int likeCount = 0) 
        {
            CommentCount = commentCount;
            LikeCount = likeCount;
        }

        public Posts(string post, int userId, string? photo = null, string? userName = null)
        {

            Post = post ?? throw new ArgumentException(nameof(post));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            UserName = userName ?? throw new ArgumentException(nameof(userName));

            var date = DateTime.Now.ToString("dd/MM/yyyy");

            Date = date;
            UserId = userId;            
        }
    }
}
