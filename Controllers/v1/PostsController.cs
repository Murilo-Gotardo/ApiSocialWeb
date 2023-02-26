using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.PostsAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:ApiVersion}/post")]
    [ApiVersion("1.0")]
    public class PostsController : ControllerBase
    {

        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostsController> _logger;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postsRepository, ILogger<PostsController> logger, IMapper mapper)
        {
            _postRepository = postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] PostsViewModel postView)
        {

            var filePath = Path.Combine("Storage", postView.Photo.FileName);

            using Stream fileSream = new FileStream(filePath, FileMode.Create);
            postView.Photo.CopyTo(fileSream);

            var contact = new Posts(postView.Name, filePath, postView.Data, postView.Post);

            _postRepository.Add(contact);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var post = _postRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(post.Photo);

            return File(dataBytes, "image/png");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var post = _postRepository.Get(pageNumber, pageQuantity);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");

            return Ok(post);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {
            var post = _postRepository.Get(id);

            var postDTOS = _mapper.Map<PostDTO>(post);

            return Ok(postDTOS);
        }

    }
}
