using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.User.Bussiness;
using CQRSPattern.Entity.User.Database;
using CQRSPattern.Services.Features.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CQRSPattern.Services.Features.Users.Command.SignIn;

public class SignInCommandHandler : ICommandHandler<SignInCommand, ICommandResult>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IIdentityTokenService _identityTokenService;
    private readonly ICustomMapper<UserModel> _userMapper;

    public SignInCommandHandler(SignInManager<User> signInManager, ICommandResultFactory commandResultFactory, IIdentityTokenService identityTokenService,
        UserManager<User> userManager, ICustomMapper<UserModel> userMapper)
    {
        _signInManager = signInManager;
        _commandResultFactory = commandResultFactory;
        _identityTokenService = identityTokenService;
        _userManager = userManager;
        _userMapper = userMapper;
    }

    public async Task<ICommandResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.SignInModel.Username);
        if (user is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound, "Invalid user name");

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.SignInModel.Password, false);
        if (!signInResult.Succeeded)
            return _commandResultFactory.Create(false, StatusCodes.Status401Unauthorized, "Invalid password");

        var roles = await _userManager.GetRolesAsync(user);

        var token = _identityTokenService.Generate(user, roles.ToList());

        var response = new SignInResponseModel
        {
            Token = token,
            UserDetails = _userMapper.AddSource(user).Map()
        };

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, response);
    }
}