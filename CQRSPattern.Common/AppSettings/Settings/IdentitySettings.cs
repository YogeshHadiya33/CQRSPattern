namespace CQRSPattern.Common.AppSettings.Settings;

public class IdentitySettings
{
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public string AllowedHosts { get; set; }
}