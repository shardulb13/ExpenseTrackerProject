using FinanceTrackingWebAPI.Model;
using FinanceTrackingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FinanceTrackingWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;
        private readonly IFriendsService _friendService;
        public FriendRequestController(IFriendRequestService friendRequestService, IFriendsService friendService)
        {
            _friendRequestService = friendRequestService;
            _friendService = friendService;
        }

        [HttpGet]
        public IActionResult GetFriendRequests()
        {
            string userId = User.Claims.First(o => o.Type == "UserID").Value;
            return Ok(_friendRequestService.GetAllFriendRequests(userId));
        }

        [HttpGet]
        [Route("FriendsData")]
        public IActionResult GetFriendsData()
        {
            string userId = User.Claims.First(o => o.Type == "UserID").Value;
            return Ok(_friendRequestService.GetFriendsData(userId));
        }

        [HttpPost]
        public IActionResult AddFriendRequest(FriendVM friend)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_friendRequestService.AddFriendRequest(friend));
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

        [HttpDelete("{friendId}")]
        public bool DeleteFriendRequest(string friendId)
        {
            return _friendRequestService.DeleteFriendRequest(friendId);
        }

        [HttpPost]
        [Route("AcceptFriendRequest")]
        public IActionResult AcceptFriendRequest(FriendVM friendData)
        {
            try
            {
                if (friendData != null)
                {

                    _friendService.AddFriend(friendData);
                    _friendRequestService.DeleteFriendRequest(friendData.UserId);
                    return Ok();
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
    }
}
