using FinanceTrackingWebAPI.Model;
using FinanceTrackingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendsService _friendsService;
        public FriendController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet]
        public IActionResult GetFriends()
        {
            string userId = User.Claims.First(o => o.Type == "UserID").Value;
            return Ok(_friendsService.GetAllFriends(userId));
        }

        [HttpGet]
        [Route("FriendsData")]
        public IActionResult GetFriendsData()
        {
            string userId = User.Claims.First(o => o.Type == "UserID").Value;
            return Ok(_friendsService.GetFriendsData(userId));
        }

        [HttpPost]
        public IActionResult AddFriend(FriendVM friend)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_friendsService.AddFriend(friend));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Failed to add friend" });
            }
        }

        [HttpDelete("{friendUserId}")]
        public bool DeleteFriend(string friendUserId)
        {
            return _friendsService.DeleteFriend(friendUserId);
        }
    }
}
