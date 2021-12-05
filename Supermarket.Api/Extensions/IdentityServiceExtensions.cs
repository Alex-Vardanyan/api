﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Supermarket.Dal.Identity;
using Supermarket.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //var builder = 
                services.AddIdentityCore<AppUser>(opt => { opt.Password.RequireNonAlphanumeric = false; opt.Password.RequiredLength = 8; })
                .AddRoles<AppRole>().AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>().AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            //builder = new IdentityBuilder(builder.UserType, builder.Services);
            //builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            //builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                    };
                });

            services.AddAuthorization(opt => {
                opt.AddPolicy("Warehouse", policy => policy.RequireRole("Warehouse Worker", "Warehouse Manager", "Master"));
                opt.AddPolicy("Warehouse Admin", policy => policy.RequireRole("Warehouse Manager", "Master"));
            });
            return services;
        }
    }
}
