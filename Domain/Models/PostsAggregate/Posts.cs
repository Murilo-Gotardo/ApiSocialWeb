using apiSocialWeb.Domain.Models.UserAggregate;
using apiSocialWeb.Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public class Posts
    {
        

        [Key]
        public int PostId { get; set; }

        public string? Data { get; set; }

        public string? Name { get; set; }

        public string? Photo { get; set; }

        public string? Post { get; set; }

        public string? Comment { get; set; }

        public string[]? CommentsData { get; set; }

        

        //[Display(Name = "User")]
        //public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User? User { get; set; }

        public Posts(string name, string photo, string post, string comments, User user)
        {
            Name = name ?? throw new ArgumentException(nameof(name));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            Data = DateTime.Now.ToString("dd/MM/yyyy");

            Post = post ?? throw new ArgumentException(nameof(post));

            CommentsData = new string[3];


            CommentsData[0] = comments;
            CommentsData[1] = user.Name;
            CommentsData[2] = user.Photo;
        }

        private Posts() { }
    }
}
