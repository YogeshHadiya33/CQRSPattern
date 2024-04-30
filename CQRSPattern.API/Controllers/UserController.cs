using CQRSPattern.API.Contracts.V1;
using CQRSPattern.Entity.User.Bussiness;
using CQRSPattern.Services.Features.Users.Command.Get;
using CQRSPattern.Services.Features.Users.Command.GetAll;
using CQRSPattern.Services.Features.Users.Command.Registration;
using CQRSPattern.Services.Features.Users.Command.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSPattern.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route(APIRoutes.User.Register)]
    public async Task<IActionResult> Register([FromBody] RegistrationModel model)
    {
        var result = await _mediator.Send(new RegisterUserCommand
        {
            Registration = model
        });
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Route(APIRoutes.User.Login)]
    public async Task<IActionResult> Login([FromBody] SignInRequestModel model)
    {
        var result = await _mediator.Send(new SignInCommand
        {
            SignInModel = model
        });
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Route(APIRoutes.User.GetAll)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersCommand());
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Route(APIRoutes.User.GetById)]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await _mediator.Send(new GetUserByIdCommand { Id = id });
        return StatusCode(result.StatusCode, result);
    }
}