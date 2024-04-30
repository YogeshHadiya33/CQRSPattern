using CQRSPattern.Common.CommandBus;

namespace CQRSPattern.Services.Features.Users.Command.Get;

public class GetUserByIdCommand : ICommand<ICommandResult>
{
    public string Id { get; set; }
}