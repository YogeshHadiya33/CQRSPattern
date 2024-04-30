using Microsoft.AspNetCore.Identity;

namespace CQRSPattern.Entity.User.Database;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
}