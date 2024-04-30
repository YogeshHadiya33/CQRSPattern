using System.Text;
using CQRSPattern.Common.AppSettings.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CQRSPattern.API.CustomInstaller;

public static class AuthenticationInstaller
{
    public static void InstallAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Jwt Authentication
        var identitySettings = new IdentitySettings();
        configuration.GetSection(nameof(IdentitySettings)).Bind(identitySettings);

        var keyValue = configuration[identitySettings?.JwtKey];
        var issuerValue = configuration[identitySettings?.JwtIssuer];
        var audienceValue = configuration[identitySettings?.JwtAudience];

        services
            .AddAuthorization()
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyValue)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}