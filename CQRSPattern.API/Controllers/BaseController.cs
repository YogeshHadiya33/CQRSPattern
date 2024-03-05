using CQRSPattern.API.Contracts.V1;
using CQRSPattern.Common.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CQRSPattern.API.Controllers;

public class BaseController : ControllerBase
{ 
    protected string UserId => User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    protected string UserName => User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    protected string UserFullName => User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
}
