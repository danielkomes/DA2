using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<User> UserService;
        public UserController(IService<User> userService)
        {
            UserService = userService;
        }
        // GET: api/<ValuesController>
        //get all
        //filtro de autenticación, sólo admins
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
        [HttpGet("{email}")]
        //get one
        //filtro de autenticación? si no es admin, solo permitir si el id == id loggeado
        //si es admin, permitir siempre
        public IActionResult Get([FromRoute] string email)
        {
            //200 ok, si el id == loggedUser.id o es admin
            //401 unauthorized, si no está loggueado o el id != loggedUser.id
            //404 not found, si es admin y no existe
            User u = new User()
            {
                Email = email
            };
            User user = UserService.Get(u);
            UserModelOut model = new UserModelOut(user);
            return Ok(model);
        }

        // POST api/<ValuesController>
        //sign in
        //pedir email y address. Si el email es invalido, dar error
        [HttpPost]
        public IActionResult Post([FromBody] UserModelIn modelIn)
        {
            //201 created, si no está loggueado y el email no está registrado
            //401 unauthorized, si ya está logueado y no es admin
            //403 forbidden, si no está loggueado y el email ya fue registrado
            UserService.Add(modelIn.ToEntity());
            return Created(modelIn.Email, "User created");
        }

        // PUT api/<ValuesController>/5
        //edit user
        //filtro de autenticación, si no es admin, solo permitir si el id == id loggeado
        //si es admin, permitir siempre
        [HttpPut("{email}")]
        public IActionResult Put([FromRoute] string email, [FromBody] UserModelIn modelIn)
        {
            //200 ok, si el id == loggedUser.id o es admin
            //401 unauthorized, si no está loggueado o no es admin
            //403 forbidden, (si el id == loggedUser.id o es admin) y el email ya fue registrado
            //404 not found, (si el id == loggedUser.id o es admin) y no existe
            UserService.Update(modelIn.ToEntity());
            return Ok("User modified");
        }

        // DELETE api/<ValuesController>/5
        //delete user
        //filtro de autenticacion, solo admins
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            //200 ok, si es admin
            //401 unauthorized, si no es admin
            //403 forbidden, si el user a borrar es admin (los admins no se pueden borrar)
            //404 not found, si es admin y no existe
            return Ok("User deleted");
        }
    }
}
