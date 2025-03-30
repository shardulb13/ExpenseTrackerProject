using FinanceTrackingWebAPI.Authentication;
using FinanceTrackingWebAPI.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;

namespace FinanceTrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserID", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            //ContentRootPath=C:\Users\IncubX\Updated miniproject\MiniProject\Backend\FinanceTrackingWebAPI\FinanceTrackingWebAPI;
            //WebRootPath = C:\Users\IncubX\Updated miniproject\MiniProject\Backend\FinanceTrackingWebAPI\FinanceTrackingWebAPI\wwwroot
            string path = Path.Combine(_hostEnvironment.WebRootPath + "\\profileImage\\");
            string uploadFile = Path.Combine(path, model.File.FileName);
            using (Stream stream = new FileStream(uploadFile, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.Firstname,
                LastName = model.Lastname,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                ProfilePhoto = "\\profileImage\\" + model.File.FileName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            string userId = User.Claims.First(o => o.Type == "UserID").Value;
            return Ok(_userManager.Users.Where(o => (o.FirstName != null && o.LastName != null) && (o.Id != userId)).ToList());
        }

    }
}
