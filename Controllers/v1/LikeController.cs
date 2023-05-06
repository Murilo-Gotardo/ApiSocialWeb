using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/like")]
    [ApiVersion("1.0")]
    public class LikeController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ILogger<LikeController> _logger;
        private readonly IMapper _mapper;

        public LikeController(IPostRepository postsRepository, ILikeRepository likeRepository, ILogger<LikeController> logger, IMapper mapper)
        {
            _postRepository = postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));
            _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] LikeViewModel likeView)
        {
            var like = new Like(likeView.UserId, likeView.PostId);

            await _likeRepository.Add(like);

            int rows = await _likeRepository.GetRows(likeView.PostId);

            var post = new Posts(null, rows);
            await _postRepository.Put(likeView.PostId, post);

            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetRows(int id)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var like = await _likeRepository.GetRows(id);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");


            return Ok(like);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _likeRepository.Delete(id);

            return Ok();
        }
    }
}
