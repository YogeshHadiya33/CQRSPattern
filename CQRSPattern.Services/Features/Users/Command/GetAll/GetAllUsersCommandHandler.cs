using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.ExtentionMethods;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.User.Bussiness;
using CQRSPattern.Entity.User.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CQRSPattern.Services.Features.Users.Command.GetAll;

public class GetAllUsersCommandHandler : ICommandHandler<GetAllUsersCommand, ICommandResult>
{
    private readonly UserManager<User> _userManager;
    private readonly ICustomMapper<List<UserModel>> _userMapper;
    private readonly ICommandResultFactory _commandResultFactory;

    public GetAllUsersCommandHandler(UserManager<User> userManager, ICustomMapper<List<UserModel>> userMapper, ICommandResultFactory commandResultFactory)
    {
        _userManager = userManager;
        _userMapper = userMapper;
        _commandResultFactory = commandResultFactory;
    }

    public async Task<ICommandResult> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync();

        if (!users.HasValue())
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound, Constants.NotFound);

        var userModels = _userMapper.AddSource(users).Map();

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, userModels);
    }
}