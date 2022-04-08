using System.Collections.Generic;
using System.Collections.Specialized;

namespace CommunityPortal.Models.Services
{
    public interface IUserService
    {
        public List<UserRoleViewModel> GetAllUsers();
        public bool DeleteUser(string Id);

        public bool UpdateRole(string Id, string role);

        public string GetRole(string Id);



    }
}
