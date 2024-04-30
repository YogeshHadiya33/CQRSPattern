using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQRSPattern.API.Filter;

public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!(context.HttpContext.User.Identity?.IsAuthenticated ?? false))
            context.Result = new UnauthorizedResult();
    }
}