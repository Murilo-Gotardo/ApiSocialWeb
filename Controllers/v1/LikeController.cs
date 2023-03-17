using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/like")]
    [ApiVersion("1.0")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ILogger<LikeController> _logger;
        private readonly IMapper _mapper;

        public LikeController(ILikeRepository likeRepository, ILogger<LikeController> logger, IMapper mapper)
        {
            _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] LikeViewModel likeView)
        {
            var like = new Like(likeView.UserId, likeView.PostId);

            _likeRepository.Add(like);

            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var like = _likeRepository.Get(id);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");


            return Ok(like);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            _likeRepository.Delete(id);

            return Ok();
        }
    }
}
