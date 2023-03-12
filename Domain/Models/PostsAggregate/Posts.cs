using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Controllers.v1;
using apiSocialWeb.Domain.Models.CommentAggregate;
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

        public string? Date { get; set; }

        public string? Name { get; set; }

        public string? Photo { get; set; }

        public string? Post { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Posts(string name, string photo, string post, int userId)
        {
            

            Name = name ?? throw new ArgumentException(nameof(name));

            Post = post ?? throw new ArgumentException(nameof(post));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            Date = date;

            UserId = userId;
        }
    }
}
