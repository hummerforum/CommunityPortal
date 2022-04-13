using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;
        private readonly UserManager<CommunityUser> userManager;

        public UserController(IUserService _userService, UserManager<CommunityUser> _userManager)
        {
            userService = _userService;
            userManager = _userManager;
        }

        public class UpdateRoleInfo
        { 
            public string UserId { get; set; }
            public string RoleId { get; set; }
        }

        [Authorize(Roles = "Admin")]

        [HttpPost("UpdateRole")]
        public bool UpdateRole([FromBody] UpdateRoleInfo info)
        {
            return userService.UpdateRole(info.UserId, info.RoleId);
        }
        
        [HttpGet("GetAllUsers")]
        public string GetAllUsers()
        {
            // lägg till roller....
            return  JsonSerializer.Serialize(userService.GetAllUsersWithRoles());
        }

        public class RoleData
        {
            public string Role { get; set; }
        }

        [HttpPost("GetRole")]
        [AllowAnonymous]
        public string GetRole()
        {
            if (this.User.IsInRole("Admin"))
                return "Admin";

            else if (this.User.IsInRole("Moderator"))
                return "Moderator";

            else if (this.User.IsInRole("User"))
                return "User";

            else
                return null;
        }


        public class DeleteInfo
        {
            public string UserId { get; set; }
        }

        [HttpPost("DeleteUser")]
        public bool DeleteUser([FromBody] DeleteInfo UserId)
        {
            if (userManager.GetUserId(User) == UserId.UserId)
                return false;


            return userService.DeleteUser(UserId.UserId);
        }
    }
}
    