using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;
        private RoleManager<IdentityRole> applicationRoleManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(IUserService _userService, RoleManager<IdentityRole> _roleManager)
        {
            userService = _userService;
            roleManager = _roleManager;

        }


        // https://localhost:44373/api/User/GetMe

        [HttpGet("GetMe")]
        public string GetMe()
        {
            return ("Makrillen simmar i havet ");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public string GetAllUsers()
        {
            // lägg till roller....

            // if(roleManager.Roles()
            return JsonSerializer.Serialize(userService.GetAllUsers());
        }

        [HttpPost]
        public bool UpdateUser(string user)
        {
            return true;
        
        }
    }
}
