using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{

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
            int i = 0;
            var  x = roleManager.Roles;

            return ("Makrillen simmar i havet ");
        }
        
        
        [HttpGet("GetAllUsers")]
        public List<CommunityUser> GetAllUsers()
        {


            return userService.GetAllUsers();
        }

    }
}
