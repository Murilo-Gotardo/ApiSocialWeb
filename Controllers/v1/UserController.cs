using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:ApiVersion}/user")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] UserViewModel userView)
        {
            var user = new User(userView.Name, userView.Email, userView.Photo, userView.Notification);

            _userRepository.Add(user);

            return Ok();
        }

     
        [HttpGet]
        [Route("get")]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var user = _userRepository.Get(pageNumber, pageQuantity);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");

            return Ok(user);
        }

     
        [HttpGet]
        [Route("search/{id}")]
        public IActionResult Search(int id)
        {
            var user = _userRepository.Get(id);

            var userDTOS = _mapper.Map<UserDTO>(user);

            return Ok(userDTOS);
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put(int id, UserViewModel userView)
        {
            var user = new User(userView.Name, userView.Email, userView.Photo, userView.Notification);

            _userRepository.Put(id, user);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);

            return Ok();
        }
    }
}
