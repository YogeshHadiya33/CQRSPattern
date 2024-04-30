using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.User.Bussiness;
using CQRSPattern.Entity.User.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CQRSPattern.Services.Features.Users.Command.Get;

public class GetUserByIdCommandHandler : ICommandHandler<GetUserByIdCommand, ICommandResult>
{
    private readonly UserManager<User> _userManager;
    private readonly ICustomMapper<UserModel> _userMapper;
    private readonly ICommandResultFactory _commandResultFactory;

    public GetUserByIdCommandHandler(ICommandResultFactory commandResultFactory, ICustomMapper<UserModel> userMapper, UserManager<User> userManager)
    {
        _commandResultFactory = commandResultFactory;
        _userMapper = userMapper;
        _userManager = userManager;
    }

    public async Task<ICommandResult> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound, Constants.NotFound);

        var userModels = _userMapper.AddSource(user).Map();

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, userModels);
    }
}