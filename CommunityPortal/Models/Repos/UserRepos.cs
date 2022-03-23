using CommunityPortal.Data;
using CommunityPortal.Models.Services;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Repos
{
    public class UserRepos: IUserservice 
    {
        private readonly ApplicationDbContext appDbContext;

        public UserRepos(ApplicationDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        public List<string> GetAllUsers()
        {
            List<string> users = new List<string>();

            List <CommunityUser>usersList = appDbContext.CommunityUsers.ToList();
            foreach (var item in usersList)
            {
                throw new System.Exception("Inte kodad");
            }


            return users;


        }
    }
}
