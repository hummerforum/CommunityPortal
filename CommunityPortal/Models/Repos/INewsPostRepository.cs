using System.Collections.Generic;

namespace CommunityPortal.Models.Repos
{

    public interface INewsPostRepository
    {
        public NewsPost Create(NewsPost newsPost);
        public List<NewsPost> Read();
        public NewsPost Read(int newsPostId);
        public NewsPost Update(NewsPost newsPost);
        public bool Delete(int newsPostId);
    }

}