using apiSocialWeb.Domain.Models.PostsAggregate;
using System.ComponentModel.DataAnnotations;

namespace apiSocialWeb.Domain.Models.CommentsAggregate
{
    public class Comments
    {

        [Key]
        public int CommentId { get; set; }

        public string? Comment { get; set; }

        public string? Name { get; set; }

        public string? Photo { get; set; }

        public Posts Posts { get; set; }


        public Comments(string comment, string name, string photo)
        {
            Name = name ?? throw new ArgumentException(nameof(name));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            Comment = comment ?? throw new ArgumentException(nameof(comment));
        }
    }
}