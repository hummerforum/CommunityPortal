using CommunityPortal.Data;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CommunityPortal.Models.Repos
{
    public class UserService: IUserService 
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly UserManager<CommunityUser> userManager;
        public UserService(ApplicationDbContext _appDbContext, UserManager<CommunityUser> _userManager)
        {
            appDbContext = _appDbContext;
            userManager = _userManager;
        }

        public bool UpdateRole(string Id, string role)
        {
            try
            {
                IdentityUserRole<string> ur = appDbContext.UserRoles.First(ur => ur.UserId == Id);
                IdentityRole ir = appDbContext.Roles.First(r => r.Name == role);
                appDbContext.UserRoles.Remove(ur);
                appDbContext.SaveChanges();

                IdentityUserRole<string> ur2 = new IdentityUserRole<string>();
                ur2.UserId = Id;
                ur2.RoleId = ir.Id;
                appDbContext.UserRoles.Add(ur2);
                appDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public List<CommunityUser> GetAllUsers()
        {
            return appDbContext.CommunityUsers.ToList();
        }

        public CommunityUser FindUserById(string id)
        {
           return appDbContext.CommunityUsers.First(cu => cu.Id == id);
        
        }


        public List<UserRoleViewModel> GetAllUsersWithRoles()
        {

            List<UserRoleViewModel> usersWithRoles =
                (from user in appDbContext.CommunityUsers
                 select new
                 {
                     UserId = user.Id,
                     Username = user.UserName,
                     Email = user.Email,
                     Telephone = user.PhoneNumber,
                     RoleName = appDbContext.Roles.First(r => r.Id == (appDbContext.UserRoles.First(ur=> ur.UserId == user.Id).RoleId)).Name

                }).ToList().Select(p => new UserRoleViewModel()

                    {
                        UserId = p.UserId,
                        UserName = p.Username,
                        Email = p.Email,
                        RoleName = p.RoleName,
                        Telephone = p.Telephone,
                    }).ToList();
            return usersWithRoles;
        }

        public bool DeleteUser(string Id)
        {
            CommunityUser user  = appDbContext.CommunityUsers.FirstOrDefault(x => x.Id == Id);
            if(user == null)
                return false;
            try
            {
                appDbContext.CommunityUsers.Remove(user);
                appDbContext.SaveChanges();

            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public string GetRole(string Id)
        {
           IdentityUserRole<string> ur = appDbContext.UserRoles.First(ur => ur.UserId == Id);
           if (ur == null)
            return "Error not found";

            IdentityRole ir = appDbContext.Roles.First(r => r.Id == ur.RoleId);

            if (ir == null)
                return "Error role not found";

            return ir.Name;
        }
    }
}
