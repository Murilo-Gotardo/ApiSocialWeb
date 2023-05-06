using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Exceptions;
using Glimpse.Core.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Common;
using System.Data.Common;
using System.Xml.Linq;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly ConnectionContext _post = new();

        public async Task Add(Posts post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post), "O Post não pode ser vazio");

            try
            {
                await _post.Posts.AddAsync(post);
                await _post.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                throw new DbUpdateException("Erro ao efetuar o Post", ex);
            }
        }

        public async Task<Posts> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do post não pode ser menor ou igual a zero");

            try
            {
                return await _post.Posts.FindAsync(id) ?? throw new ResourceNotFoundException(id);
            }
            catch (DbException ex)
            {
                throw new SearchEmptyException($"A pesquisa não retornou dados para o post de ID {id}", ex);
            }
        }

        public async Task<List<Posts>> Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "O número da página não pode ser menor ou igual a zero");

            if (pageQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageQuantity), "A quantidade de itens por página não pode ser menor ou igual a zero");

            try
            {
                return await _post.Posts
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync();
            }
            catch (DbException ex)
            {
                throw new SearchEmptyException("A pesquisa não retornou dados", ex);
            }
        }

        public async Task<List<Posts>> Get(int userId, int pageNumber, int pageQuantity)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId), "O ID do usuário não pode ser menor ou igual a zero");

            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "O número da página não pode ser menor ou igual a zero");

            if (pageQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageQuantity), "A quantidade de itens por página não pode ser menor ou igual a zero");

            try
            {
                return await _post.Posts
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync();
            }
            catch (DbException ex) 
            {
                throw new SearchEmptyException($"A pesquisa não retornou dados com o contexto de usuario, de ID {userId}", ex);
            }
        }

        public async Task<bool> Put(int id, Posts post)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do post não pode ser menor ou igual a zero");

            try
            {
                var existingPost = await _post.Posts.FirstOrDefaultAsync(p => p.PostId == id);

                if (existingPost != null)
                {
                    // Update the properties of the existing post
                    if (post.Photo != null && post.Post_txt != null)
                    {
                        existingPost.Photo = post.Photo;
                        existingPost.Post_txt = post.Post_txt;
                    }
                    else if (post.CommentCount != null)
                    {
                        existingPost.CommentCount = post.CommentCount;

                    }
                    else if (post.LikeCount != null)
                    {
                        existingPost.LikeCount = post.LikeCount;
                    }

                    _post.Posts.Update(existingPost);
                    await _post.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A atualização do post, de ID {id}, falhou", ex);
            }          
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do post não pode ser menor ou igual a zero");

            try
            {
                var post = await _post.Posts.FirstOrDefaultAsync(p => p.PostId == id) ?? throw new Exception($"Post with ID {id} not found.");
                _post.Posts.RemoveRange(post);
                var affectedRows = await _post.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A exclusão do post, de ID {id}, falhou", ex);
            }
        }
    }
}