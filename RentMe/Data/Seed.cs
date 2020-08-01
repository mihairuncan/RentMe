using Microsoft.AspNetCore.Identity;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentMe.Data
{
    public class Seed
    {
        public static void SeedData(RoleManager<Role> roleManager, UserManager<User> userManager, DatabaseContext context)
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

            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    FirstName = "Mihai",
                    LastName = "Runcan",
                    Email = "runcan.mihai@gmail.com",
                    UserName = "admin",
                    PhoneNumber = "0754695879",
                    DateOfBirth = new DateTime(1993, 5, 7),
                    City = "Cluj-Napoca",
                    Country = "Romania",
                    Created = DateTime.Now,
                    LastActive = DateTime.Now
                };
                userManager.CreateAsync(user, "password").Wait();
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                        {
                            Id = new Guid(),
                            Name = "real-estate",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "apartments"},
                                                    new Subcategory(){ Id = new Guid(), Name = "houses"},
                                                    new Subcategory(){ Id = new Guid(), Name = "commercial"},
                                                }
                        },
                    new Category
                        {
                            Id = new Guid(),
                            Name = "vehicles",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "cars"},
                                                    new Subcategory(){ Id = new Guid(), Name = "caravans"},
                                                    new Subcategory(){ Id = new Guid(), Name = "motorcycles"},
                                                    new Subcategory(){ Id = new Guid(), Name = "bicycles"},
                                                    new Subcategory(){ Id = new Guid(), Name = "electric-scooters"},
                                                }
                        },
                     new Category
                        {
                            Id = new Guid(),
                            Name = "games",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "desktop"},
                                                    new Subcategory(){ Id = new Guid(), Name = "playStation"},
                                                    new Subcategory(){ Id = new Guid(), Name = "xbox"},
                                                }
                        },
                    new Category
                        {
                            Id = new Guid(),
                            Name = "clothes-and-accessories",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "clothes-for-men"},
                                                    new Subcategory(){ Id = new Guid(), Name = "clothes-for-women"},
                                                    new Subcategory(){ Id = new Guid(), Name = "clothes-for-kids"}

                                                }
                        },
                    new Category
                        {
                            Id = new Guid(),
                            Name = "others",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "books"},
                                                    new Subcategory(){ Id = new Guid(), Name = "board-games"},
                                                }
                        }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }
    }
}
