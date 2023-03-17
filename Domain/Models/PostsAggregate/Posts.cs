using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        public string? Date { get; set; }

        public string? Photo { get; set; }

        public string? Post { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> comments { get; set; }

        public ICollection<Like> likes { get; set; }
        

        public Posts(string photo, string post, int userId)
        {

            Post = post ?? throw new ArgumentException(nameof(post));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            var date = DateTime.Now.ToString("dd/MM/yyyy");

            Date = date;
            UserId = userId;

            
        }
    }
}
