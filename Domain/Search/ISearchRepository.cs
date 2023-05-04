using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Domain.Search
{
    public interface ISearchRepository
    {
        string Search(string key);
    }
}
