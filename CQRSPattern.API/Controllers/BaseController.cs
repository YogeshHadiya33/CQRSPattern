using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CQRSPattern.API.Controllers;

public class BaseController : ControllerBase
{
    protected string UserId => User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    protected string UserName => User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    protected string UserFullName => User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
}