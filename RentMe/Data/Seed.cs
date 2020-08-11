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
                                                    new Subcategory(){ Id = new Guid(), Name = "apartments", DisplayName="Apartments"},
                                                    new Subcategory(){ Id = new Guid(), Name = "houses", DisplayName="Houses"},
                                                    new Subcategory(){ Id = new Guid(), Name = "commercial", DisplayName="Commercial Spaces"},
                                                }
                        },
                    new Category
                        {
                            Id = new Guid(),
                            Name = "vehicles",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "cars", DisplayName="Cars"},
                                                    new Subcategory(){ Id = new Guid(), Name = "caravans", DisplayName="Caravans"},
                                                    new Subcategory(){ Id = new Guid(), Name = "motorcycles", DisplayName="Motorcycles"},
                                                    new Subcategory(){ Id = new Guid(), Name = "bicycles", DisplayName="Bicycles"},
                                                    new Subcategory(){ Id = new Guid(), Name = "electric-scooters", DisplayName="Electric Scooters"},
                                                }
                        },
                     new Category
                        {
                            Id = new Guid(),
                            Name = "games",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "desktop", DisplayName="Desktop Games"},
                                                    new Subcategory(){ Id = new Guid(), Name = "playStation", DisplayName="PlayStation Games"},
                                                    new Subcategory(){ Id = new Guid(), Name = "xbox", DisplayName="Xbox Games"},
                                                }
                        },
                    new Category
                        {
                            Id = new Guid(),
                            Name = "clothes-and-accessories",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "clothes-for-men", DisplayName="Clothes for men"},
                                                    new Subcategory(){ Id = new Guid(), Name = "clothes-for-women", DisplayName="Clothes for women"},
                                                    new Subcategory(){ Id = new Guid(), Name = "clothes-for-kids", DisplayName="Clothes for kids"}

                                                }
                        },
                    new Category
                        {
                            Id = new Guid(),
                            Name = "others",
                            Subcategories = new List<Subcategory>
                                                {
                                                    new Subcategory(){ Id = new Guid(), Name = "books", DisplayName="Books"},
                                                    new Subcategory(){ Id = new Guid(), Name = "board-games", DisplayName="Board Games"},
                                                }
                        }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }
    }
}
