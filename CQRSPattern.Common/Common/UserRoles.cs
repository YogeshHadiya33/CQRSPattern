using System.ComponentModel;

namespace CQRSPattern.Common.Common;

public enum UserRoles
{
    [Description("Admin")] Admin,
    [Description("User")] User
}