using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace apiSocialWeb.Domain.Models.CommentAggregate
{
    public class Comment
    {

        [Key]
        public int CommentId { get; set; }

        public string? Icomment { get; set; }

        public string? UserName { get; set; }

        public string? Photo { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Posts Post { get; set; }


        public Comment()
        {
        }

        public Comment (string iComment, string userName, string photo, int postId, int userId)
        {
            Icomment = iComment ?? throw new ArgumentException(nameof(iComment));
            UserName = userName ?? throw new ArgumentException(nameof(userName));
            Photo = photo ?? throw new ArgumentException(nameof(photo));
            UserId = userId;
            PostId = postId;
        }
    }
}
