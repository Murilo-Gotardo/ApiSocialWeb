using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.CommentAggregate;
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
        public async Task<IActionResult> Add([FromBody] PostsViewModel postView)
        {
            var user = await _userRepository.Get(postView.UserId);

            var post = new Posts(postView.Post, postView.UserId, postView.Photo, user.Name_txt);

            await _postRepository.Add(post);

            return Ok();
        }

        [HttpPost]
        [Route("download/{id}")]
        public async Task<IActionResult> DownloadPhoto(int id)
        {
            var post = await _postRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(post.Photo);

            return File(dataBytes, "image/png");
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(int pageNumber, int pageQuantity)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var post = await _postRepository.Get(pageNumber, pageQuantity);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");

 
            return Ok(post);
        }

        [HttpGet]
        [Route("getuserpost/{id}")]
        public async Task<IActionResult> Get(int id, int pageNumber, int pageQuantity)
        {
            var userPosts = await _postRepository.Get(id, pageNumber, pageQuantity);

            return Ok(userPosts);
        }

        [HttpGet]
        [Route("search/{id}")]
        public async Task<IActionResult> Search(int id)
        {
            var post = await _postRepository.Get(id);
            var postDTOS = _mapper.Map<PostDTO>(post);
            return Ok(postDTOS);
        }

        [HttpPut]
        [Route("put/{id}")]
        public async Task<IActionResult> Put(int id, PostsViewModel postView) 
        {
            var post = new Posts(postView.Post, postView.UserId, postView.Photo);

            await _postRepository.Put(id, post);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _postRepository.Delete(id);

            return Ok();
        }
    }
}
