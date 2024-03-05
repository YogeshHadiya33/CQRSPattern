using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSPattern.Entity.User.Bussiness;

public class SignInResponseModel
{
    public UserModel UserDetails { get; set; }
    public string Token { get; set; }
}
