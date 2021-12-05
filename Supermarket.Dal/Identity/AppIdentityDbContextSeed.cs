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
        public static async Task SeedUserAsync(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var roles = new List<AppRole>
                {
                    new AppRole{Name = "Master"},
                    new AppRole{Name = "Warehouse Manager"},
                    new AppRole{Name = "Warehouse Worker"},
                    new AppRole{Name = "Client"}
                };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
                var master = new AppUser
                {
                    Email = "alexandervardanyan@test.com",
                    UserName = "AlexanderVardanyan"
                };
                await userManager.CreateAsync(master, "S0meth1ng");
                await userManager.AddToRoleAsync(master, "Master");

                var user = new AppUser
                {
                    Email = "testclient@test.com",
                    UserName = "TestClient"
                };
                await userManager.CreateAsync(user, "Passw0rd");
                await userManager.AddToRoleAsync(user, "Client");
            }
            
        }
    }
}
