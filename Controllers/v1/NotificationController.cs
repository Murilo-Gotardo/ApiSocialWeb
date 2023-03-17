using apiSocialWeb.Application.ViewModel;
using apiSocialWeb.Domain.Models.NotificationAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/notification")]
    [ApiVersion("1.0")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notifyRepository;
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        public NotificationController(INotificationRepository notifyRepository, ILogger<NotificationController> logger, IMapper mapper)
        {
            _notifyRepository = notifyRepository ?? throw new ArgumentNullException(nameof(notifyRepository));
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] NotificationViewModel notifyView)
        {
            var notify = new Notification(notifyView.NotificationType, notifyView.Status, notifyView.PostId, notifyView.UserId);

            _notifyRepository.Add(notify);

            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id)
        {

            //_logger.Log(LogLevel.Error, "erro");

            var notify = _notifyRepository.Get(id);

            //_logger.LogInformation("teste");

            //throw new Exception("erro");


            return Ok(notify);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            _notifyRepository.Delete(id);

            return Ok();
        }
    }
}
