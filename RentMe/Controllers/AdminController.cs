using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMe.Models;

namespace RentMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await _userManager.Users
                                        .OrderBy(u => u.UserName)
                                        .Select(user => new
                                        {
                                            user.Id,
                                            user.UserName,
                                            Roles = user.UserRoles.Select(ur=>ur.Role.Name).ToList()
                                        })
                                        .ToListAsync();
                                        
            return Ok(userList);
        }
    }
}
