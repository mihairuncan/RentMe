using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentMe.Data;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(string userId);
        public Task<PagedList<User>> GetUsersWithRoles(UserParams userParams);
        public Task<IEnumerable<string>> EditRoles(string userName, RolesForEdit rolesForEdit);
        public Task<User> GetUserByUsernameAndEmail(string username, string email);
        public Task<bool> SaveAll();
        public Task<bool> ChangePassword(User user, string password);
    }

    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> GetUserByUsernameAndEmail(string username, string email)
        {
            return await _context.Users
                                .FirstOrDefaultAsync(u => u.UserName == username && u.Email == email);
        }

        public async Task<User> GetUser(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<PagedList<User>> GetUsersWithRoles(UserParams userParams)
        {
            var users = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userParams.Username))
            {
                users = users.Where(u => u.UserName.Contains(userParams.Username));
            }

            users = users.OrderBy(u => u.UserName);

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IEnumerable<string>> EditRoles(string userName, RolesForEdit rolesForEdit)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = rolesForEdit.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                throw new Exception("Failed to add to roles");
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                throw new Exception("Failed to remove the roles");
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePassword(User user, string password)
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, password);

            return true;
        }
    }
}
