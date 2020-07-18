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
        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role{ Name="Regular" },
                    new Role{ Name="Moderator" },
                    new Role{ Name="Admin" },
                    new Role{ Name="VIP" },
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }
    }
}
