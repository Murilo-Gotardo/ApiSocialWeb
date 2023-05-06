using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using apiSocialWeb.Domain.Search;
using apiSocialWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ConnectionContext _search = new();

        public string Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) 
                throw new ArgumentNullException(nameof(key), "A chave de pesquisa não pode ser vazia");

            try
            {
                var query = from p in _search.Posts
                            join u in _search.User on p.UserId equals u.UserId
                            join c in _search.Comment on p.PostId equals c.PostId
                            where p.Post_txt.Contains(key) || p.UserName_txt.Contains(key) || u.Name_txt.Contains(key) || c.Icomment_txt.Contains(key) || c.UserName_txt.Contains(key)
                            select new { Post = p, User = u, Comment = c };

                var results = query.ToList();

                var posts = new List<Posts>();
                var users = new List<User>();
                var comment = new List<Comment>();

                foreach (var result in results)
                {
                    posts.Add(result.Post);
                    users.Add(result.User);
                    comment.Add(result.Comment);
                }

                if (posts.Count == 0 && users.Count == 0 && comment.Count == 0)
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

                    var json = JsonSerializer.Serialize(new { Posts = posts, Users = users, Comment = comment }, options);
                    return json;
                }
            }
            catch (DbException ex)
            {
                throw new SearchEmptyException($"A pesquisa não retornou nenhum registro, chave usada: {key}", ex);
            }
        }
    }
}
