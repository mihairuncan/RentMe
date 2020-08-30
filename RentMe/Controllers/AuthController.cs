using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.Services;
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
        private readonly IUserService _userService;
        private readonly SmtpClient _smtpClient;

        public AuthController(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration config, IUserService userService,
            SmtpClient smtpClient)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _userService = userService;
            _smtpClient = smtpClient;
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserForPasswordRecorer userForPasswordRecorer)
        {
            var user = await _userService.GetUserByUsernameAndEmail(userForPasswordRecorer.Username, userForPasswordRecorer.Email);

            if (user == null)
            {
                Thread.Sleep(3000);
                return Ok();
            }

            string password = GeneratePassword();
            var passwordChanged = await _userService.ChangePassword(user, password);

            if (passwordChanged)
            {
                await _smtpClient.SendMailAsync(
                     new MailMessage(
                         "runcan.mihai@gmail.com",
                         user.Email,
                         "Reset Password Rent Me",
                         "You have requested a new password for your Rent Me account with username <b><u>" + user.UserName + "</u></b>. Your new password is <b><u>" + password + "</u></b>")
                     {
                         IsBodyHtml = true
                     }); ;
            }

            return Ok();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] Password password)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userService.GetUser(userId);

            if (password.PasswordText?.Length <= 4)
            {
                return BadRequest("Invalid Password");
            }

            if (user == null)
            {
                return BadRequest();
            }

            var passwordChanged = await _userService.ChangePassword(user, password.PasswordText);

            if (passwordChanged)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            userForRegister.DateOfBirth = userForRegister.DateOfBirth.AddHours(3);
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

        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = 8;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}
