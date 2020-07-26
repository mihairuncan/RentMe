using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.ViewModels;

namespace RentMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;


        public AuthController(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration config)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            if (userForRegister.DateOfBirth.CalculateAge() < 16)
            {
                return BadRequest("You should have at least 16 years old!");
            }

            var user = await _userManager.FindByNameAsync(userForRegister.Username);
            if (user != null)
            {
                return BadRequest("Username already taken");
            };


            var userToCreate = _mapper.Map<User>(userForRegister);
            userToCreate.Created = DateTime.Now;
            userToCreate.LastActive = DateTime.Now;

            var result = await _userManager.CreateAsync(userToCreate, userForRegister.Password);
            await _userManager.AddToRoleAsync(userToCreate, "Regular");

            var userToReturn = _mapper.Map<UserProfileDetails>(userToCreate);

            if (result.Succeeded)
            {
                return Ok(userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var user = await _userManager.FindByNameAsync(userForLogin.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username/pasword");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

            if (result.Succeeded)
            {
                var appUser = _mapper.Map<UserProfileDetails>(user);

                return Ok(new
                {
                    token = await GenerateJwtToken(user),
                    user = appUser
                });
            }

            return Unauthorized("Invalid username/pasword");
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Secret").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
