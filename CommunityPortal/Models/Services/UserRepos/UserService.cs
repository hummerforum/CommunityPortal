using CommunityPortal.Data;
using CommunityPortal.Models.Services;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Repos
{
    public class UserService: IUserService 
    {
        private readonly ApplicationDbContext appDbContext;

        public UserService(ApplicationDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        public List<CommunityUser> GetAllUsers()
        {
            return  appDbContext.CommunityUsers.ToList();
        }


        public bool UpdateUser(string userInfo)
        {
            // vilken info skall kunna uppdateras, tommy behöver säga vad. 

            return false;
        }


        public bool RemoveUser(string Id)
        {
            CommunityUser user  = appDbContext.CommunityUsers.FirstOrDefault(x => x.Id == Id);
            if(user == null)
                return false;
            try
            {
                appDbContext.CommunityUsers.Remove(user);
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}
