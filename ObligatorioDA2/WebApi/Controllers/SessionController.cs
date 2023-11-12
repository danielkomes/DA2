using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/session")]
    [ApiController]
    [ExceptionFilter]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic SessionLogic;
        public SessionController(ISessionLogic sessionService)
        {
            SessionLogic = sessionService;
        }

        [HttpPost("{email}")]
        public IActionResult Login([FromRoute] string email, [FromBody] string password)
        {
            User user = new User()
            {
                Email = email,
                Password = password
            };
            Guid token = SessionLogic.Authenticate(user);
            var ret = new
            {
                result = "Logged in",
                token = token
            };
            return Ok(ret);
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpDelete]
        public IActionResult Logout()
        {
            SessionLogic.Logout();
            return Ok();
        }
    }
}
