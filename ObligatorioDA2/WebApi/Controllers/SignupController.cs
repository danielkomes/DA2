using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;

namespace WebApi.Controllers
{
    [Route("api/signup")]
    [ApiController]
    [ExceptionFilter]
    public class SignupController : ControllerBase
    {
        private readonly IService<User> UserService;
        public SignupController(IService<User> userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public IActionResult Signup([FromBody] UserModelIn modelIn)
        {
            bool exists = UserService.Exists(modelIn.ToEntity());
            if (!exists)
            {
                UserService.Add(modelIn.ToEntity());
                return Created(modelIn.Email, "User created");
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Email already exists");
            }
        }
    }
}
