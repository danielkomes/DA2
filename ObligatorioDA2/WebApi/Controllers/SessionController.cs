using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic SessionLogic;
        public SessionController(ISessionLogic sessionService)
        {
            SessionLogic = sessionService;
        }

        // POST api/<ValuesController>
        //sign in
        //pedir email y address. Si el email es invalido, dar error
        [HttpPost]
        public IActionResult Login([FromBody] string email, [FromBody] string password)
        {
            //200 ok, si el email existe
            //201 created, si no está loggueado y el email no está registrado
            User user = new User()
            {
                Email = email
            };
            SessionLogic.Authenticate(user);
            return Ok("Logged in");
        }
        [HttpDelete]
        public IActionResult Logout()
        {
            //200 ok, si está loggeado
            //401 unauthorized, si no está loggeado
            SessionLogic.Logout();
            return Ok("Logged out");
        }
    }
}
