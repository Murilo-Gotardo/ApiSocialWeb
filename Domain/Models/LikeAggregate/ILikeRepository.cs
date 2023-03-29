namespace apiSocialWeb.Domain.Models.LikeAggregate
{
    public interface ILikeRepository
    {

        void Add(Like like);

        int GetRows(int id);

        Task Delete(int id);
    }
}
