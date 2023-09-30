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
                        return StatusCode(403, "Profile mismatch");
                    }
                    else
                    {
                        return Ok(new UserModelOut(user));
                    }

                }
                catch (ResourceNotFoundException)
                {
                    return StatusCode(403, "Profile mismatch");
                }
            }
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] UserModelIn modelIn)
        {
            //TODO: 401 unauthorized, si ya está logueado y no es admin
            bool exists = UserService.Exists(modelIn.ToEntity());
            if (!exists)
            {
                //201 created, si no está loggueado y el email no está registrado
                UserService.Add(modelIn.ToEntity());
                return Created(modelIn.Email, "User created");
            }
            else
            {
                //403 TODO: forbidden, si no está loggueado y el email ya fue registrado
                return StatusCode(403, "Email already exists");
            }
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpPut("{email}")]
        public IActionResult Put([FromRoute] string email, [FromBody] UserModelIn modelIn)
        {
            //200 ok, si el id == loggedUser.id o es admin
            //401 unauthorized, si no está loggueado o no es admin
            //403 forbidden, (si el id == loggedUser.id o es admin) y el email ya fue registrado
            //404 not found, (si el id == loggedUser.id o es admin) y no existe
            //TODO: verificar email
            User current = SessionLogic.GetCurrentUser();
            User newUser = modelIn.ToEntity();
            bool exists = UserService.Exists(newUser);
            User oldUser = new User()
            {
                Email = email
            };
            //oldUser = UserService.Get(oldUser);
            if (current.Roles.Contains(EUserRole.Admin))
            {
                oldUser = UserService.Get(oldUser);
                newUser.Id = oldUser.Id;
                if (exists) return StatusCode(403, "Email already exists");
                UserService.Update(newUser);
                return Ok("User modified");
            }
            else
            {
                try
                {
                    oldUser = UserService.Get(oldUser);
                    if (current.Id != oldUser.Id)
                    {
                        return StatusCode(403, "Profile mismatch");
                    }
                    else
                    {
                        if (exists) return StatusCode(403, "Email already exists");
                        newUser.Id = oldUser.Id;
                        UserService.Update(newUser);
                        return Ok("User modified");
                    }
                }
                catch (ResourceNotFoundException)
                {
                    return StatusCode(403, "Profile mismatch");
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
