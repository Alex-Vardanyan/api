using Microsoft.AspNetCore.Identity;
using Supermarket.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Dal.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "alexandervardanyan@test.com",
                    UserName = "AlexanderVardanyan",
                    Role = "Master"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
            
        }
    }
}
