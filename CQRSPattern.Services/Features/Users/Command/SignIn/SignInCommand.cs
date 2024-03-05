using CQRSPattern.Common.CommandBus;
using CQRSPattern.Entity.User.Bussiness;
using System.Windows.Input;

namespace CQRSPattern.Services.Features.Users.Command.SignIn;

public class SignInCommand : ICommand<ICommandResult>
{
    public SignInRequestModel SignInModel { get; set; }
}
