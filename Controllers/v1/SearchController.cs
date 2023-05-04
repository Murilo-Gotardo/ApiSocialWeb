using apiSocialWeb.Domain.Models.UserAggregate;
using apiSocialWeb.Domain.Search;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/Search")]
    [ApiVersion("1.0")]
    public class SearchController : ControllerBase
    {

        private readonly ISearchRepository _searchRepository;


        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository ?? throw new ArgumentNullException(nameof(searchRepository));
            
        }


        [HttpGet]
        [Route("search/{key}")]
        public IActionResult Search(string key)
        {
            var search = _searchRepository.Search(key);
         
            return Ok(search);

        }
    }
}
