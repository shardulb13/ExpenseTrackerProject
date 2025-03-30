
using FinanceTrackingWebAPI.Authentication;
using FinanceTrackingWebAPI.Data;
using FinanceTrackingWebAPI.Model;
using FinanceTrackingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserProfileController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _userManager = userManager;
            _hostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<object> GetUserProfile()
        {
            string userId = User.Claims.First(o => o.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.ProfilePhoto
            };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromForm] Register model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string userId = User.Claims.First(o => o.Type == "UserID").Value;
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        string path = Path.Combine(_hostEnvironment.WebRootPath + "\\profileImage\\");
                        string uploadFile = Path.Combine(path, model.File.FileName);
                        using (Stream stream = new FileStream(uploadFile, FileMode.Create))
                        {
                            model.File.CopyTo(stream);

                        }
                        user.FirstName = model.Firstname;
                        user.LastName = model.Lastname;
                        user.UserName = model.UserName;
                        user.Email = model.Email;
                        user.ProfilePhoto = "\\profileImage\\" + model.File.FileName;
                        var result = await _userManager.UpdateAsync(user);
                        return Ok(result);

                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Id not found" });
            }
        }
    }

}
