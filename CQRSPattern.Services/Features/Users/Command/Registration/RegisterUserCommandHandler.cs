using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.User.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CQRSPattern.Services.Features.Users.Command.Registration;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ICommandResult>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ICommandResultFactory _commandResultFactory;

    public RegisterUserCommandHandler(UserManager<User> userManager, ICommandResultFactory commandResultFactory, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _commandResultFactory = commandResultFactory;
        _roleManager = roleManager;
    }

    public async Task<ICommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var identityUserResult = await _userManager.CreateAsync(new User
        {
            UserName = request.Registration.Email,
            Email = request.Registration.Email,
            FirstName = request.Registration.FirstName,
            LastName = request.Registration.LastName,
            DateOfBirth = DateOnly.Parse(request.Registration.DateOfBirth)
        }, request.Registration.Password);

        if (!identityUserResult.Succeeded)
            return _commandResultFactory.Create(false, StatusCodes.Status500InternalServerError, identityUserResult.Errors.Select(x => x.Description).ToList());

        var user = await _userManager.FindByEmailAsync(request.Registration.Email);
        if (user is null)
            return _commandResultFactory.Create(false, StatusCodes.Status500InternalServerError, "Can not create user at a moment");

        if (!await _roleManager.RoleExistsAsync(UserRoles.User.ToString()))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));

        await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());

        return _commandResultFactory.Create(true, "User registered successfully", user.Id, StatusCodes.Status200OK);
    }
}