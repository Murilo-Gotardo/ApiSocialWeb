using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.NotificationAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Exceptions;
using Glimpse.Core.Extensibility;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ConnectionContext _notify = new();

        public async Task Add(Notification notify)
        {
            try
            {
                await _notify.Notifications.AddAsync(notify);
                await _notify.SaveChangesAsync();
            }
            catch (DbException ex) 
            {
                throw new DbUpdateException("Erro ao efetuar a notificação", ex);
            }
        }

        public async Task<List<Notification>> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID da notificação não pode ser menor ou igual a zero");

            try
            {
                return await _notify.Notifications
                .Where(p => p.PostId == id)
                .ToListAsync() ?? throw new ResourceNotFoundException(id);
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A pesquisa não retornou resultados para a notificação de ID {id}", ex);
            }
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID da notificação não pode ser menor ou igual a zero");

            try
            {
                var notify = await _notify.Notifications.FirstOrDefaultAsync(n => n.NotificationId == id) ?? throw new ResourceNotFoundException(id);

                //Se a remoção for bem-sucedida, o número de linhas afetadas é igual a 1. Se for 0, é lançada uma exceção ResourceNotFoundException
                var entry = _notify.Notifications.Remove(notify);
                await _notify.SaveChangesAsync();
                var affectedRows = entry.State == EntityState.Deleted ? 1 : 0;

                if (affectedRows == 0)
                    throw new ResourceNotFoundException(id);
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A exclusão da notificação, de ID {id}, falhou", ex);
            }
        }
    }
}
