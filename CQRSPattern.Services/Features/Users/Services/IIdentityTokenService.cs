using CQRSPattern.Entity.User.Database;

namespace CQRSPattern.Services.Features.Users.Services;

public interface IIdentityTokenService
{
    string Generate(User user, List<string> userRole);
}