using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.Services;
using RentMe.ViewModels;

namespace RentMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IMapper mapper, UserManager<User> userManager)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("usersWithRoles")]
        [Authorize(Policy = "ModerateRole")]
        public async Task<IActionResult> GetUsersWithRoles([FromQuery] UserParams userParams)
        {
            var users = await _userService.GetUsersWithRoles(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserWithRoles>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpPost("editRoles/{userName}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EditRoles(string userName, RolesForEdit rolesForEdit)
        {
            return Ok(await _userService.EditRoles(userName, rolesForEdit));
        }
    }
}
