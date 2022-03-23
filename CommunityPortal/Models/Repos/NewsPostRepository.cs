using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Repos
{

    public class NewsPostRepository : INewsPostRepository
    {
        private static int idCounter = 0;
        private static List<NewsPost> NewsPosts = new List<NewsPost>();

        public NewsPost Create(NewsPost newsPost)
        {
            NewsPost newNewsPost = new NewsPost();
            newNewsPost = newsPost;
            newNewsPost.NewsPostId = ++idCounter;
            NewsPosts.Add(newNewsPost);
            return newNewsPost;
        }

        public List<NewsPost> Read()
        {
            return NewsPosts;
        }

        public NewsPost Read(int newsPostId)
        {
            return NewsPosts.FirstOrDefault(np => np.NewsPostId == newsPostId);
        }

        public NewsPost Update(NewsPost newsPost)
        {
            int id = newsPost.NewsPostId;
            NewsPost newsPostToUpdate = Read(id);
            if (newsPostToUpdate == null)
                return null;

            newsPostToUpdate = newsPost;
            newsPostToUpdate.NewsPostId = id;
            return newsPostToUpdate;
        }

        public bool Delete(int newsPostId)
        {
            NewsPost newsPostToDelete = Read(newsPostId);
            if (newsPostToDelete == null)
                return false;

            return NewsPosts.Remove(newsPostToDelete);
        }
    }

}