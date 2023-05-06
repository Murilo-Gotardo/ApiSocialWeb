using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.CircuitBreaker;

namespace apiSocialWeb.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/comment")]
    [ApiVersion("1.0")]
    public class CommentController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentController> _logger;
        private readonly IMapper _mapper;

        public CommentController(IUserRepository userRepository, IPostRepository postsRepository, ICommentRepository commentRepository, ILogger<CommentController> logger, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _postRepository = postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] CommentViewModel commentView)
        {
            var user = await _userRepository.Get(commentView.UserId);

            var comment = new Comment(commentView.Icomment, commentView.Photo, commentView.PostId, commentView.UserId, user.Name_txt);

            await _commentRepository.Add(comment);

            int rows = await _commentRepository.GetRows(commentView.PostId);

            var post = new Posts(rows);
            await _postRepository.Put(commentView.PostId, post);

            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(int id)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var comment = await _commentRepository.Get(id);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");


            return Ok(comment);
        }

        [HttpGet]
        [Route("getpc/{id}")]

        public async Task<IActionResult> Get(int id, int pageNumber, int pageQuantity)
        {
            var comment = await _commentRepository.Get(id, pageNumber, pageQuantity);
            return Ok(comment);
        }

        [HttpGet]
        [Route("getrow/{id}")]

        public async Task<IActionResult> GetRows(int id)
        {
            var rows = await _commentRepository.GetRows(id);

            return Ok(rows);
        }

        [HttpPut]
        [Route("put/{id}")]
        public async Task<IActionResult> Put(int id, CommentViewModel commentView)
        {
            var comment = new Comment(commentView.Icomment, commentView.Photo, commentView.PostId, commentView.UserId);

            await _commentRepository.Put(id, comment);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _commentRepository.Delete(id);

            return Ok();
        }
    }
}
