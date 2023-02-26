using apiSocialWeb.Application.Services;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers
{

    [ApiController]
    [Route("api/v{version:ApiVersion}/auth")]
    public class AuthController : Controller
    {
        [HttpPost]

        public IActionResult Auth(string username, string password)
        {
            if (username == "la" && password == "123")
            {
                var token = TokenService.GenerateToken(new User("la", "email", "Storage/nao", "saeg"));
                return Ok(token);
            }
            else
            {
                return BadRequest("ta ruim");
            }

        }
    }
}
