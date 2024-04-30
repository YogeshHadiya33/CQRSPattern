using CQRSPattern.Common.CommandBus;
using CQRSPattern.Entity.User.Bussiness;

namespace CQRSPattern.Services.Features.Users.Command.Registration;

public class RegisterUserCommand : ICommand<ICommandResult>
{
    public RegistrationModel Registration { get; set; }
}