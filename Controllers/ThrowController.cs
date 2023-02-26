using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace apiSocialWeb.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]

    public class ThrowController : ControllerBase
    {
        [Route("/error")]

        public ActionResult HandleError() => 
            Problem();


        [Route("/error-development")]

        public IActionResult HandleErrorDevelopment(
            [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = 
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }
        
    }
}
