using BusinessLogic;
using DataAccess.Exceptions;
using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IService<User> UserService;
        private readonly ISessionLogic SessionLogic;
        public UserController(IService<User> userService, ISessionLogic sessionLogic)
        {
            UserService = userService;
            SessionLogic = sessionLogic;
        }
        // GET: api/<ValuesController>
        //get all
        //filtro de autenticación, sólo admins

        [ServiceFilter(typeof(AuthenticationFilter))]
        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            //200 ok (o 204 no content), si es admin
            //401 unauthorized, si no está loggueado o no es admin
            IEnumerable<UserModelOut> models = new List<UserModelOut>();
            IEnumerable<User> users = UserService.GetAll();
            foreach (User user in users)
            {
                UserModelOut model = new UserModelOut(user);
                models = models.Append(model);
            }
            return Ok(models);
        }

        // GET api/<ValuesController>/5
        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpGet("{email}")]
        //get one
        //filtro de autenticación? si no es admin, solo permitir si el id == id loggeado
        //si es admin, permitir siempre
        public IActionResult Get([FromRoute] string email)
        {
            //200 ok, si el id == loggedUser.id o es admin
            //TODO: 401 unauthorized, si no está loggueado o el id != loggedUser.id
            //TODO: 404 not found, si es admin y no existe
            User u = new User()
            {
                Email = email
            };

            User current = SessionLogic.GetCurrentUser();
            if (current.Roles.Contains(EUserRole.Admin))
            {
                User user = UserService.Get(u);
                UserModelOut model = new UserModelOut(user);
                return Ok(model);
            }
            else
            {
                try
                {
                    User user = UserService.Get(u);
                    if (user.Id != current.Id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
                    }
                    else
                    {
                        return Ok(new UserModelOut(user));
                    }

                }
                catch (ResourceNotFoundException)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
                }
            }
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] UserModelIn modelIn)
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

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpPut("{email}")]
        public IActionResult Put([FromRoute] string email, [FromBody] UserModelIn modelIn)
        {
            User current = SessionLogic.GetCurrentUser();
            User newUser = modelIn.ToEntity();
            bool exists = UserService.Exists(newUser);
            User oldUser = new User()
            {
                Email = email
            };
            if (current.Roles.Contains(EUserRole.Admin)) //is admin
            {
                oldUser = UserService.Get(oldUser);
                newUser.Id = oldUser.Id;
                if (exists) return StatusCode(StatusCodes.Status403Forbidden, "Email already exists");
                UserService.Update(newUser);
                return Ok("User modified");
            }
            else //is not admin 
            {
                try
                {
                    oldUser = UserService.Get(oldUser);
                    if (current.Id != oldUser.Id) //it's not their profile
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
                    }
                    else //it's their own profile
                    {
                        if (exists) return StatusCode(StatusCodes.Status403Forbidden, "Email already exists");
                        newUser.Id = oldUser.Id;
                        UserService.Update(newUser);
                        return Ok("User modified");
                    }
                }
                catch (ResourceNotFoundException) //the other profile does not exist
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Profile mismatch");
                }
            }
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpDelete("{email}")]
        public IActionResult Delete([FromRoute] string email)
        {
            //200 ok, si es admin
            //401 unauthorized, si no es admin
            //403 forbidden, si el user a borrar es admin (los admins no se pueden borrar)
            //404 not found, si es admin y no existe
            User user = new User() { Email = email };
            UserService.Delete(user);
            return Ok("User deleted");
        }
    }
}
