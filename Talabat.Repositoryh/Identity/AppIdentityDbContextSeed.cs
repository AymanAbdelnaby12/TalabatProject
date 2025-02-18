using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUser(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) 
            {
                var User = new AppUser
                {
                    DisplayName = "Ayman",
                    Email = "ayman@Test.com",
                    UserName = "ayman",
                    PhoneNumber = "1234567890",

                };
                userManager.CreateAsync(User, "Pa$$w0rd").Wait();
            }
            
         
        }
    }
}
