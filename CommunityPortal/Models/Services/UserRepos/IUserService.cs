using System.Collections.Generic;
using System.Collections.Specialized;

namespace CommunityPortal.Models.Services
{
    public interface IUserService
    {
        public List<UserRoleViewModel> GetAllUsersWithRoles();
        public List<CommunityUser> GetAllUsers();

        public CommunityUser FindUserById(string id);

        public bool DeleteUser(string Id);

        public bool UpdateRole(string Id, string role);

        public string GetRole(string Id);

            




    }
}
