namespace CQRSPattern.Entity.User.Bussiness;

public class SignInResponseModel
{
    public UserModel UserDetails { get; set; }
    public string Token { get; set; }
}