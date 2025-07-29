using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Market.Application.Authentication
{
    public static class ServiceCollectionExtentions
    {
        public static void AddAuthToken(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly",
                    policy =>
                    {
                        policy.RequireRole("Admin");
                        policy.RequireRole("Editor");
                    });
                options.AddPolicy("UserOnly", policy =>
                {
                    policy.RequireRole("User");
                    policy.RequireRole("ProUser");
                });
            }
                );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = AuthOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                };
            });
        }
    }
}
