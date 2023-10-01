using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpPost]
        public IActionResult Login([FromBody] string email)
        {
            User user = new User()
            {
                Email = email
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
            //200 ok, si está loggeado
            //401 TODO: (via filter) unauthorized, si no está loggeado
            SessionLogic.Logout();
            return Ok("Logged out");
        }
    }
}
