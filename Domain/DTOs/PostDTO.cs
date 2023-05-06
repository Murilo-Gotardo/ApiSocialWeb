using apiSocialWeb.Domain.Models.CommentAggregate;

namespace apiSocialWeb.Domain.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Post { get; set; }

        public string Data { get; set; }

        public Comment Comment { get; set; }
    }
}
