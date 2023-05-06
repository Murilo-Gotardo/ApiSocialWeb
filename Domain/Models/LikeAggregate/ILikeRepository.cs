namespace apiSocialWeb.Domain.Models.LikeAggregate
{
    public interface ILikeRepository
    {
        Task Add(Like like);

        Task<int> GetRows(int id);

        Task Delete(int id);
    }
}
