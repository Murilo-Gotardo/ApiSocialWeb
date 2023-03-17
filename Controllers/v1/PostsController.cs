using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/posts")]
    [ApiVersion("1.0")]
    public class PostsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostsController> _logger;
        private readonly IMapper _mapper;

        public PostsController(IUserRepository userRepository, IPostRepository postsRepository, ILogger<PostsController> logger, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _postRepository = postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] PostsViewModel postView)
        {
            var post = new Posts(postView.Photo, postView.Post, postView.UserId);

            _postRepository.Add(post);

            return Ok();
        }

     
        [HttpPost]
        [Route("download/{id}")]
        public IActionResult DownloadPhoto(int id)
        {
            var post = _postRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(post.Photo);

            return File(dataBytes, "image/png");
        }
       
        [HttpGet]
        [Route("get")]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var post = _postRepository.Get(pageNumber, pageQuantity);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");

 
            return Ok(post);
        }

        [HttpGet]
        [Route("search/{id}")]
        public IActionResult Search(int id)
        {
            var post = _postRepository.Get(id);

            var postDTOS = _mapper.Map<PostDTO>(post);

            return Ok(postDTOS);
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put(int id, PostsViewModel postView) 
        {
            var post = new Posts(postView.Post, postView.Photo, postView.UserId);

            _postRepository.Put(id, post);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            _postRepository.Delete(id);

            return Ok();
        }
    }
}
