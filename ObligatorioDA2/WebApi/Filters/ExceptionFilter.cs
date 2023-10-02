using DataAccess.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (ResourceNotFoundException e)
            {
                context.Result = new ObjectResult(new { e.Message }) { StatusCode = StatusCodes.Status404NotFound };
            }
            catch (InvalidOperationException e)
            {
                context.Result = new ObjectResult(new { e.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            catch (InvalidDataException e)
            {
                context.Result = new ObjectResult(new { e.Message }) { StatusCode = StatusCodes.Status403Forbidden };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Message: {e.Message} - StackTrace: {e.StackTrace}");

                context.Result = new ObjectResult(new
                {
                    Message = "Unexpected error" +
                    Environment.NewLine +
                    e.Message +
                    Environment.NewLine +
                    "Stack trace: " +
                    Environment.NewLine +
                    e.StackTrace
                })
                { StatusCode = 500 };
            }
        }
    }
}

