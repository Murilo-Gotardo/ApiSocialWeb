using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.PostsAggregate;
using Glimpse.Core.Extensibility;
using Microsoft.EntityFrameworkCore;

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
                    CommentData = b.CommentData
                   
                }).ToList();
        }

        public Posts? Get(int id)
        {
            return _post.Posts.Find(id);
        }

        public async Task<bool> Put(int id, Posts post)
        {
            var existingPost = await _post.Posts.FirstOrDefaultAsync(p => p.PostId == id);

            if (existingPost != null)
            {
                // Update the properties of the existing post
                existingPost.Name = post.Name;
                existingPost.Photo = post.Photo;
                existingPost.Post = post.Post;

                _post.Posts.Update(existingPost);
                await _post.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task Delete(int postId)
        {

            var post = await _post.Posts.FirstOrDefaultAsync(p => p.PostId == postId) ?? throw new Exception($"Post with ID {postId} not found.");
            _post.Posts.Remove(post);

            await _post.SaveChangesAsync();
        }
    }
}