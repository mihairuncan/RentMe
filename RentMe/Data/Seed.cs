using Microsoft.AspNetCore.Identity;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Data
{
    public class Seed
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole{ Name="Regular" },
                    new IdentityRole{ Name="Moderator" },
                    new IdentityRole{ Name="Admin" },
                    new IdentityRole{ Name="VIP" },
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }
    }
}
