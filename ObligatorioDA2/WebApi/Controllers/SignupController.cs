using Domain;
using IBusinessLogic;
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
        private readonly ISignupLogic SignupLogic;
        public SignupController(ISignupLogic signupLogic)
        {
            SignupLogic = signupLogic;
        }

        [HttpPost]
        public IActionResult Signup([FromBody] UserModelInForCustomers modelIn)
        {
            SignupLogic.Signup(modelIn.ToEntity());
            return Created(modelIn.Email, modelIn);
        }
    }
}
