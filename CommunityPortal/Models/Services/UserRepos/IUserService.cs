using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{
    public interface IUserService
    {
        public List<CommunityUser> GetAllUsers();

        public CommunityUser FindUserById(string userId);

        public bool UpdateUser(string userInfo);

        public bool RemoveUser(string Id);
    }
}
