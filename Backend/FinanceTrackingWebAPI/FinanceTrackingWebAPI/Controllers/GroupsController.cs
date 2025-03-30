using FinanceTrackingWebAPI.Model;
using FinanceTrackingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        public IActionResult Groups()
        {
            string userId = User.FindFirstValue(ClaimTypes.Name);
            return Ok(_groupService.GetAllGroups(userId));
        }

        [HttpPost]
        public async Task<IActionResult> AddGroups(GroupVM groupModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _groupService.CreateGroup(groupModel));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Group Creation Failed" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(GroupVM groupModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _groupService.UpdateGroup(groupModel));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Id not Found" });
            }
        }

        [HttpDelete("{id}")]
        public bool DeleteGroup(int id)
        {
            return _groupService.DeleteGroup(id);
        }

        [HttpDelete]
        [Route("DeleteGroupUser/{userId}")]
        public bool DeleteGroupUser(string userId)
        {
            return _groupService.DeleteGroupUser(userId);
        }
    }
}
