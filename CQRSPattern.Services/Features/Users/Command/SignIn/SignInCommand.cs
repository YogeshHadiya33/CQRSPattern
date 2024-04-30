using CQRSPattern.Common.CommandBus;
using CQRSPattern.Entity.User.Bussiness;

namespace CQRSPattern.Services.Features.Users.Command.SignIn;

public class SignInCommand : ICommand<ICommandResult>
{
    public SignInRequestModel SignInModel { get; set; }
}