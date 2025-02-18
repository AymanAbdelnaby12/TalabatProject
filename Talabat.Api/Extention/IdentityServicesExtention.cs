using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.Api.Extention
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<ITokenService,TokenService>();
            Services.AddIdentity<AppUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppIdentityDbContext>();
            // allow dependency injection for authentication for(UserManager , SigininManger, RoleManger)
            Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidateIssuerSigningKey = true
                         
               
                    };
                });
            return Services;
        }
    }
}
