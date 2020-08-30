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
                    new Role{ Name="Moderator" },
                    new Role{ Name="Admin" }
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


                var apartmentsId = context.Subcategories.FirstOrDefault(s => s.Name == "apartments").Id;
                var housesId = context.Subcategories.FirstOrDefault(s => s.Name == "houses").Id;
                var commercialId = context.Subcategories.FirstOrDefault(s => s.Name == "commercial").Id;
                var carsId = context.Subcategories.FirstOrDefault(s => s.Name == "cars").Id;
                var caravansId = context.Subcategories.FirstOrDefault(s => s.Name == "caravans").Id;
                var motorcyclesId = context.Subcategories.FirstOrDefault(s => s.Name == "motorcycles").Id;
                var bicyclesId = context.Subcategories.FirstOrDefault(s => s.Name == "bicycles").Id;
                var electricScootersId = context.Subcategories.FirstOrDefault(s => s.Name == "electric-scooters").Id;
                var desktopId = context.Subcategories.FirstOrDefault(s => s.Name == "desktop").Id;
                var playStationId = context.Subcategories.FirstOrDefault(s => s.Name == "playStation").Id;
                var xboxId = context.Subcategories.FirstOrDefault(s => s.Name == "xbox").Id;
                var clothesForMenId = context.Subcategories.FirstOrDefault(s => s.Name == "clothes-for-men").Id;
                var clothesForWomenId = context.Subcategories.FirstOrDefault(s => s.Name == "clothes-for-women").Id;
                var clothesForKidsId = context.Subcategories.FirstOrDefault(s => s.Name == "clothes-for-kids").Id;
                var booksId = context.Subcategories.FirstOrDefault(s => s.Name == "books").Id;
                var boardGamesId = context.Subcategories.FirstOrDefault(s => s.Name == "board-games").Id;

                var userId = userManager.Users.FirstOrDefault(u => u.UserName == "admin").Id;


                var announcement = new Announcement
                {
                    Id = new Guid(),
                    Title = "Renting Apartment",
                    Description = "",
                    RentPrice = 350,
                    RentPeriod = "month",
                    AddedOn = DateTime.Now,
                    IsApproved = true,
                    IsActive = true,
                    PostedById = userId,
                    SubcategoryId = apartmentsId
                };

                context.Announcements.AddRange(
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 1",
                              Description = "",
                              RentPrice = 350,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 2",
                              Description = "",
                              RentPrice = 200,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 3",
                              Description = "",
                              RentPrice = 250,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 4",
                              Description = "",
                              RentPrice = 550,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 5",
                              Description = "",
                              RentPrice = 850,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 6",
                              Description = "",
                              RentPrice = 150,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 7",
                              Description = "",
                              RentPrice = 200,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 8",
                              Description = "",
                              RentPrice = 500,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 1",
                              Description = "",
                              RentPrice = 350,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 2",
                              Description = "",
                              RentPrice = 200,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 3",
                              Description = "",
                              RentPrice = 250,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 4",
                              Description = "",
                              RentPrice = 550,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 5",
                              Description = "",
                              RentPrice = 850,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 6",
                              Description = "",
                              RentPrice = 150,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 7",
                              Description = "",
                              RentPrice = 200,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 8",
                              Description = "",
                              RentPrice = 500,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 1",
                              Description = "",
                              RentPrice = 1350,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 2",
                              Description = "",
                              RentPrice = 2200,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 3",
                              Description = "",
                              RentPrice = 2250,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 4",
                              Description = "",
                              RentPrice = 1550,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 5",
                              Description = "",
                              RentPrice = 1850,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 6",
                              Description = "",
                              RentPrice = 2150,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 7",
                              Description = "",
                              RentPrice = 1200,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 8",
                              Description = "",
                              RentPrice = 2500,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },

                           new Announcement
                           {
                               Id = new Guid(),
                               Title = "Renting Dress 1",
                               Description = "",
                               RentPrice = 35,
                               RentPeriod = "week",
                               AddedOn = DateTime.Now,
                               IsApproved = true,
                               IsActive = true,
                               PostedById = userId,
                               SubcategoryId = clothesForWomenId
                           },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 2",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 3",
                              Description = "",
                              RentPrice = 25,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 4",
                              Description = "",
                              RentPrice = 55,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 5",
                              Description = "",
                              RentPrice = 85,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 6",
                              Description = "",
                              RentPrice = 15,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 7",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Dress 8",
                              Description = "",
                              RentPrice = 50,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForWomenId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 1",
                              Description = "",
                              RentPrice = 30,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 2",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 3",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 4",
                              Description = "",
                              RentPrice = 50,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 5",
                              Description = "",
                              RentPrice = 80,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 6",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 7",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 8",
                              Description = "",
                              RentPrice = 50,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 1",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 2",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 3",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 4",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 5",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 6",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 7",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 8",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },

                           new Announcement
                           {
                               Id = new Guid(),
                               Title = "Renting pc game 1",
                               Description = "",
                               RentPrice = 3,
                               RentPeriod = "week",
                               AddedOn = DateTime.Now,
                               IsApproved = true,
                               IsActive = true,
                               PostedById = userId,
                               SubcategoryId = desktopId
                           },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 2",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 3",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 4",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 5",
                              Description = "",
                              RentPrice = 8,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 6",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 7",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting pc game 8",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = desktopId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 1",
                              Description = "",
                              RentPrice = 3,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 2",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 3",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 4",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 5",
                              Description = "",
                              RentPrice = 8,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 6",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 7",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting PlayStation game 8",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = playStationId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 1",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 2",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 3",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 4",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 5",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 6",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 7",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 8",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },



                           new Announcement
                           {
                               Id = new Guid(),
                               Title = "Renting car 1",
                               Description = "",
                               RentPrice = 53,
                               RentPeriod = "week",
                               AddedOn = DateTime.Now,
                               IsApproved = true,
                               IsActive = true,
                               PostedById = userId,
                               SubcategoryId = carsId
                           },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 2",
                              Description = "",
                              RentPrice = 52,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 3",
                              Description = "",
                              RentPrice = 52,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 4",
                              Description = "",
                              RentPrice = 55,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 5",
                              Description = "",
                              RentPrice = 58,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 6",
                              Description = "",
                              RentPrice = 51,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 7",
                              Description = "",
                              RentPrice = 52,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 8",
                              Description = "",
                              RentPrice = 55,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 1",
                              Description = "",
                              RentPrice = 83,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 2",
                              Description = "",
                              RentPrice = 82,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 3",
                              Description = "",
                              RentPrice = 82,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 4",
                              Description = "",
                              RentPrice = 85,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 5",
                              Description = "",
                              RentPrice = 88,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 6",
                              Description = "",
                              RentPrice = 81,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 7",
                              Description = "",
                              RentPrice = 82,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 8",
                              Description = "",
                              RentPrice = 85,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 1",
                              Description = "",
                              RentPrice = 21,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 2",
                              Description = "",
                              RentPrice = 22,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 3",
                              Description = "",
                              RentPrice = 22,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 4",
                              Description = "",
                              RentPrice = 21,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 5",
                              Description = "",
                              RentPrice = 21,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 6",
                              Description = "",
                              RentPrice = 22,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 7",
                              Description = "",
                              RentPrice = 21,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 8",
                              Description = "",
                              RentPrice = 22,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 1",
                              Description = "",
                              RentPrice = 3,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 2",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 3",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 4",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 5",
                              Description = "",
                              RentPrice = 8,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 6",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 7",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 8",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 1",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 2",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 3",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 4",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 5",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 6",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 7",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 8",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },






                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 1",
                              Description = "",
                              RentPrice = 3,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 2",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 3",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 4",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 5",
                              Description = "",
                              RentPrice = 8,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 6",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 7",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 8",
                              Description = "",
                              RentPrice = 5,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 1",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 2",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 3",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 4",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 5",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 6",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 7",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 8",
                              Description = "",
                              RentPrice = 2,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = true,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },

                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting book 9",
                              Description = "",
                              RentPrice = 3,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = booksId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting board game 9",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = boardGamesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting electric scooter 9",
                              Description = "",
                              RentPrice = 10,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = electricScootersId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting bicycle 9",
                              Description = "",
                              RentPrice = 3,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = bicyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting motorcycle 9",
                              Description = "",
                              RentPrice = 21,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = motorcyclesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting caravan 9",
                              Description = "",
                              RentPrice = 83,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = caravansId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting car 9",
                              Description = "",
                              RentPrice = 53,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = carsId
                          },
                           new Announcement
                           {
                               Id = new Guid(),
                               Title = "Renting PlayStation game 9",
                               Description = "",
                               RentPrice = 5,
                               RentPeriod = "week",
                               AddedOn = DateTime.Now,
                               IsApproved = false,
                               IsActive = true,
                               PostedById = userId,
                               SubcategoryId = playStationId
                           },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting xbox game 9",
                              Description = "",
                              RentPrice = 1,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = xboxId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Halloween Costume 9",
                              Description = "",
                              RentPrice = 20,
                              RentPeriod = "day",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForKidsId
                          },
                           new Announcement
                           {
                               Id = new Guid(),
                               Title = "Renting pc game 9",
                               Description = "",
                               RentPrice = 3,
                               RentPeriod = "week",
                               AddedOn = DateTime.Now,
                               IsApproved = false,
                               IsActive = true,
                               PostedById = userId,
                               SubcategoryId = desktopId
                           },
                           new Announcement
                           {
                               Id = new Guid(),
                               Title = "Renting Dress 9",
                               Description = "",
                               RentPrice = 50,
                               RentPeriod = "week",
                               AddedOn = DateTime.Now,
                               IsApproved = false,
                               IsActive = true,
                               PostedById = userId,
                               SubcategoryId = clothesForWomenId
                           },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Suit 9",
                              Description = "",
                              RentPrice = 30,
                              RentPeriod = "week",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = clothesForMenId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting House 9",
                              Description = "",
                              RentPrice = 500,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = housesId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Commercial Space 9",
                              Description = "",
                              RentPrice = 1350,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = commercialId
                          },
                          new Announcement
                          {
                              Id = new Guid(),
                              Title = "Renting Apartment 9",
                              Description = "",
                              RentPrice = 350,
                              RentPeriod = "month",
                              AddedOn = DateTime.Now,
                              IsApproved = false,
                              IsActive = true,
                              PostedById = userId,
                              SubcategoryId = apartmentsId
                          }
                    );
                context.SaveChanges();
            }
        }
    }
}
