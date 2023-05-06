using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using apiSocialWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ConnectionContext _user = new();

        public async Task Add(User user)
        {
            try
            {
                await _user.User.AddAsync(user);
                await _user.SaveChangesAsync();
            }
            catch (DbException ex) 
            {
                throw new DbUpdateException("O cadastro de usuário falhou", ex);
            }            
        }

        public async Task<User?> Get(int id)
        {
            if (id <= 0)
                throw new ResourceNotFoundException(id);

            try
            {
                return await _user.User.FindAsync(id) ?? throw new ResourceNotFoundException(id);
            }
            catch (DbException ex)
            {
                throw new SearchEmptyException($"A pesquisa não retornou dados para o user de ID {id}", ex);
            }
        }

        public async Task<List<User>> Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "O número da página não pode ser menor ou igual a zero");

            if (pageQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageQuantity), "A quantidade de itens por página não pode ser menor ou igual a zero");

            try
            {
                return await _user.User
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync();
            }
            catch (DbException ex)
            {
                throw new SearchEmptyException("A pesquisa não retornou dados", ex);
            }
        }

        public async Task<bool> Put(int id, User user)
        {
            if (id <= 0)
                throw new ResourceNotFoundException(id);

            try
            {
                var existingUser = await _user.User.FirstOrDefaultAsync(u => u.UserId == id) ?? throw new ResourceNotFoundException(id);

                if (existingUser != null)
                {
                    // Update the properties of the existing post
                    existingUser.Name_txt = user.Name_txt;
                    existingUser.Email = user.Email;
                    existingUser.Photo = user.Photo;

                    var entry = _user.User.Update(existingUser);
                    await _user.SaveChangesAsync();

                    var affectedRows = entry.State == EntityState.Modified ? 1 : 0;

                    if (affectedRows == 0)
                        throw new ResourceNotFoundException(id);

                    return true;
                }

                return false;
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A atualização do usuario, de ID {id}, falhou", ex);
            }
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new ResourceNotFoundException(id);

            try
            {
                var user = await _user.User.FirstOrDefaultAsync(u => u.UserId == id) ?? throw new ResourceNotFoundException(id);
                _user.User.RemoveRange(user);
                var affectedRows = await _user.SaveChangesAsync();

                if (affectedRows == 0)
                    throw new ResourceNotFoundException(id);
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A exclusão do usuario, de ID {id}, falhou", ex);
            }
        }
    }
}
