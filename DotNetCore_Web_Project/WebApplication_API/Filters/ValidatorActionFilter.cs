using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net.Mime;

namespace WebApplication_API.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid) {
                if (context.HttpContext.Request.Method == "Get")
                {
                    var result = new BadRequestResult();
                    context.Result = result;
                }
                else {
                    var result = new ContentResult();
                    string content = JsonConvert.SerializeObject(context.ModelState,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }
                        );

                    result.Content = content;
                    result.ContentType = MediaTypeNames.Application.Json;

                    context.HttpContext.Response.StatusCode = 400;
                    context.Result = result;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
