using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic UserLogic;
        public UserController(IUserLogic userLogic)
        {
            UserLogic = userLogic;
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = UserLogic.GetAll();
            IEnumerable<UserModelOutForAdmins> models = new List<UserModelOutForAdmins>();
            foreach (User user in users)
            {
                UserModelOutForAdmins model = new UserModelOutForAdmins(user);
                models = models.Append(model);
            }
            return Ok(models);
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpGet("{email}")]
        public IActionResult Get([FromRoute] string email)
        {
            User user = UserLogic.Get(email);
            UserModelOutForCustomers model = new UserModelOutForCustomers(user);
            return Ok(model);
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] UserModelInForAdmins modelIn)
        {
            UserLogic.Add(modelIn.ToEntity());
            return Created(modelIn.Email, modelIn);
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpPut("{email}")]
        public IActionResult Put([FromRoute] string email, [FromBody] UserModelInForCustomers modelIn)
        {
            UserLogic.Update(email, modelIn.ToEntity());
            return Ok();
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpDelete("{email}")]
        public IActionResult Delete([FromRoute] string email)
        {
            UserLogic.Delete(email);
            return Ok();
        }
    }
}
