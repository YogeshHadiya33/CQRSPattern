using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CQRSPattern.Common.AppSettings.Genric;
using CQRSPattern.Common.AppSettings.Settings;
using CQRSPattern.Entity.User.Database;
using Microsoft.IdentityModel.Tokens;

namespace CQRSPattern.Services.Features.Users.Services;

public class IdentityTokenService : IIdentityTokenService
{
    private readonly IGenericAppSettings<IdentitySettings> _identitySettings;

    public IdentityTokenService(IGenericAppSettings<IdentitySettings> identitySettings)
    {
        _identitySettings = identitySettings;
    }

    public string Generate(User user, List<string> userRole)
    {
        var settings = _identitySettings.GetAppSettings();

        var jwtKey = _identitySettings.GetAppSettingValue(settings.JwtKey);
        var issuer = _identitySettings.GetAppSettingValue(settings.JwtIssuer);
        var audience = _identitySettings.GetAppSettingValue(settings.JwtAudience);

        var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.UserName)
            }),
            Expires = DateTime.Now.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKeyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenJson = tokenHandler.WriteToken(token);

        return tokenJson;
    }
}