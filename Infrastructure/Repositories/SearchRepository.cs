using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using apiSocialWeb.Domain.Search;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ConnectionContext _search = new ConnectionContext();

        public string Search(string key)
        {
            var query = from p in _search.Posts
                        join u in _search.User on p.UserId equals u.UserId
                        where p.Post_txt.Contains(key) || p.UserName_txt.Contains(key) || u.Name_txt.Contains(key)
                        select new { Post = p, User = u };

            var posts = new List<Posts>();
            var users = new List<User>();

            foreach (var result in query)
            {
                posts.Add(result.Post);
                users.Add(result.User);
            }

            if (posts.Count == 0 && users.Count == 0)
            {
                var json = JsonSerializer.Serialize("A pesquisa não retornou resultados");
                return json;
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true //opcional para facilitar a leitura do JSON
                };

                var json = JsonSerializer.Serialize(new { Posts = posts, Users = users }, options);
                return json;
            }

        }
    }
}
