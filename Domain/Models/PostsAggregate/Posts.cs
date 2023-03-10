using apiSocialWeb.Controllers.v1;
using apiSocialWeb.Domain.Models.UserAggregate;
using apiSocialWeb.Infrastructure.Repositories;
using AutoMapper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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

        public string[] CommentData { get; set; }

        //[Display(Name = "User")]
        //public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User? User { get; set; }

        public Posts(string name, string photo, string post, string[] commentData)
        {
            

            Name = name ?? throw new ArgumentException(nameof(name));

            Post = post ?? throw new ArgumentException(nameof(post));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            Data = date;
            

            CommentData = commentData;
        }
    }
}
