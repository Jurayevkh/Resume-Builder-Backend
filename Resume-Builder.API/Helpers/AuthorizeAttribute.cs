using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Resume_Builder.Domain.Entities.User;

namespace Resume_Builder.API.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (Users?)context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" });
    }
}

