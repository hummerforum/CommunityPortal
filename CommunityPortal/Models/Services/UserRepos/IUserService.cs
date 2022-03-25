using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{
    public interface IUserService
    {
        public List<CommunityUser> GetAllUsers();

        public bool RemoveUser(string Id);
    }
}
