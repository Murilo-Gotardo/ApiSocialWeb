using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.PostsAggregate;
using static apiSocialWeb.Domain.Models.PostsAggregate.Posts;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly ConnectionContext _post = new ConnectionContext();
        public void Add(Posts post)
        {
            _post.Posts.Add(post);
            _post.SaveChanges();
        }

        public List<PostDTO> Get(int pageNumber, int pageQuantity)
        {
            return _post.Posts.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Select(b =>
                new PostDTO()
                {
                    Id = b.PostId,
                    Name = b.Name,
                    Post = b.Post,
                    Data = b.Data,
                    Comments = b.CommentsData
                   
                }).ToList();
        }

        public Posts? Get(int id)
        {
            return _post.Posts.Find(id);
        }
    }
}