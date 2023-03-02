using apiSocialWeb.Domain.Models.UserAggregate;

namespace apiSocialWeb.Application.ViewModel
{
    public class PostsViewModel
    {
        public string? Name { get; set; }

        public string? Post { get; set; }

        public IFormFile? Photo { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }
    }
}
