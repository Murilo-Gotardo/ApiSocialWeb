﻿using apiSocialWeb.Application.ViewModel;
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
        public IActionResult Add([FromBody] CommentViewModel commentView)
        {

            var user = _userRepository.Get(commentView.UserId);

            var comment = new Comment(commentView.Icomment, commentView.Photo, commentView.PostId, commentView.UserId, user.Name);



            _commentRepository.Add(comment);


            int rows = _commentRepository.GetRows(commentView.PostId);

            var post = new Posts(rows);
            _postRepository.Put(commentView.PostId, post);

            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get(int id)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var comment = _commentRepository.Get(id);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");


            return Ok(comment);
        }

        [HttpGet]
        [Route("getrow/{id}")]

        public IActionResult GetRows(int id)
        {
            var rows = _commentRepository.GetRows(id);

            return Ok(rows);
        }

        [HttpGet]
        [Route("getpc/{id}")]
        public IActionResult Get(int id, int pageNumber, int pageQuantity)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var comment = _commentRepository.GetPostComments(id, pageNumber, pageQuantity);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");


            return Ok(comment);
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put(int id, CommentViewModel commentView)
        {
            var comment = new Comment(commentView.Icomment, commentView.Photo, commentView.PostId, commentView.UserId);

            _commentRepository.Put(id, comment);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);

            return Ok();
        }
    }
}
