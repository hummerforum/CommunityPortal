using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{

    public interface INewsPostService
    {
        public NewsPost Add(NewsPost newsPost);
        public NewsPost GetById(int newsPostId);
        public List<NewsPost> GetList();
        List<NewsPost> SearchOR(string searchString);
        List<NewsPost> SearchAND(string searchString);
        public bool Delete(int newsPostId);
    }

}