using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Eventing.Reader;

namespace WebApi.Filters
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionLogic SessionLogic;
        public AuthenticationFilter(ISessionLogic sessionLogic)
        {
            SessionLogic = sessionLogic;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var header = context.HttpContext.Request.Headers["Authorization"].ToString();
            Guid token = Guid.Empty;

            //if (string.IsNullOrEmpty(header) || !Guid.TryParse(header, out token))
            //{
            //    context.Result = new ObjectResult(new { Message = "Authorization header missing" })
            //    {
            //        StatusCode = StatusCodes.Status401Unauthorized
            //    };
            //}
            //else
            {
                User currentUser = SessionLogic.GetCurrentUser(token);
                if (currentUser is null)
                {
                    context.Result = new ObjectResult(new { Message = "Unauthorized" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
        }
    }
}
