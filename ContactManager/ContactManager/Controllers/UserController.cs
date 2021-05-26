using ContactManager.DTO;
using ContactManager.Models;
using ContactManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ContactDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;

        //UserController constructor
        public UserController(ContactDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IOptionsMonitor<JwtConfig> optionsMonitor)                              
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Registers User
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            //Creating New User
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            //Checks if Role exists , Creates Role if it doesn't exist
            var role = await _roleManager.RoleExistsAsync("Regular User");
            if (!role)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Regular User" });
            }

            if (!result.Succeeded)
            {
                return BadRequest("something went wrong");
            }
            //Add Role to User
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok("Registration successful");
        }

        /// <summary>
        /// Logs in User
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //Finds User
            var user = _context.Users.FirstOrDefault(a => a.Email == model.Email);

            //Logs in User
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                return BadRequest("something went wrong");
            }

            //Generates Token
            var token = GetToken(user);
            if (token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }

        /// <summary>
        /// Generates Token with JWT
        /// </summary>
        private async Task<string> GetToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.MySecret);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Union(roleClaims)
               ),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
