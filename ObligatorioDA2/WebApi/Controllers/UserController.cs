using DataAccess.Exceptions;
using Domain;
using IBusinessLogic;
using IDataAccess;
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

            //User current = SessionLogic.GetCurrentUser();
            //if (current.Roles.Contains(EUserRole.Admin)) //is admin
            //{
            //    User user = UserService.Get(u);
            //    UserModelOut model = new UserModelOut(user);
            //    return Ok(model);
            //}
            //else //is not admin
            //{
            //    try
            //    {
            //        User user = UserService.Get(u);
            //        if (user.Id != current.Id) //is not their own profile
            //        {
            //            return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
            //        }
            //        else
            //        {
            //            return Ok(new UserModelOut(user)); //is their profile
            //        }

            //    }
            //    catch (ResourceNotFoundException) //not found
            //    {
            //        return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
            //    }
            //}
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] UserModelInForCustomers modelIn)
        {

            UserLogic.Add(modelIn.ToEntity());
            return Created(modelIn.Email, "User created");


            //bool exists = UserService.Exists(modelIn.ToEntity());

            //if (!exists)
            //{
            //    UserService.Add(modelIn.ToEntity());
            //    return Created(modelIn.Email, "User created");
            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status403Forbidden, "Email already exists");
            //}
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpPut("{email}")]
        public IActionResult Put([FromRoute] string email, [FromBody] UserModelInForCustomers modelIn)
        {
            UserLogic.Update(modelIn.ToEntity());
            return Ok("User modified");

            //User current = SessionLogic.GetCurrentUser();
            //User newUser = modelIn.ToEntity();
            //bool exists = UserService.Exists(newUser);
            //User oldUser = new User()
            //{
            //    Email = email
            //};
            //if (current.Roles.Contains(EUserRole.Admin)) //is admin
            //{
            //    oldUser = UserService.Get(oldUser);
            //    newUser.Id = oldUser.Id;
            //    if (exists) return StatusCode(StatusCodes.Status403Forbidden, "Email already exists");
            //    UserService.Update(newUser);
            //    return Ok("User modified");
            //}
            //else //is not admin 
            //{
            //    try
            //    {
            //        oldUser = UserService.Get(oldUser);
            //        if (current.Id != oldUser.Id) //it's not their profile
            //        {
            //            return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
            //        }
            //        else //it's their own profile
            //        {
            //            if (exists) return StatusCode(StatusCodes.Status403Forbidden, "Email already exists");
            //            newUser.Id = oldUser.Id;
            //            UserService.Update(newUser);
            //            return Ok("User modified");
            //        }
            //    }
            //    catch (ResourceNotFoundException) //the other profile does not exist
            //    {
            //        return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
            //    }
            //}
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpDelete("{email}")]
        public IActionResult Delete([FromRoute] string email)
        {
            UserLogic.Delete(email);
            return Ok("User deleted");
        }
    }
}
