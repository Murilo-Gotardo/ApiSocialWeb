using apiSocialWeb.Domain.Models.CommentsAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public class Posts
    {

        [Key]
        public int PostId { get; set; }

        public string Data { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public string Post { get; set; }


        [ForeignKey("CommentsId")]
        public int CommentsId { get; set; }

        public Comments Comments { get; set; }

        public Posts(string name, string photo, string data, string post)
        {
            Name = name ?? throw new ArgumentException(nameof(name));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            Data = data ?? throw new ArgumentException(nameof(data));

            Post = post ?? throw new ArgumentException(nameof(post));
        }
    }
}
