using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public EUserRole RoleNeeded { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            Guid token = Guid.Empty;

            //if (string.IsNullOrEmpty(authorizationHeader) || !Guid.TryParse(authorizationHeader, out token))
            //{
            //    context.Result = new ObjectResult(new { Message = "Authorization header missing" })
            //    {
            //        StatusCode = StatusCodes.Status401Unauthorized
            //    };
            //}

            ISessionLogic sessionLogic = GetSessionLogic(context);
            User currentUser = sessionLogic.GetCurrentUser(token);

            if (currentUser is null)
            {
                context.Result = new ObjectResult(new { Message = "Not authenticated" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
            else if (!currentUser.Roles.Contains(RoleNeeded))
            {
                context.Result = new ObjectResult(new { Message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }

        protected ISessionLogic GetSessionLogic(AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetService(typeof(ISessionLogic));
            var sessionLogic = sessionService as ISessionLogic;

            return sessionLogic;
        }
    }
}