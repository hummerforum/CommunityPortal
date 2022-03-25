using System.Collections.Generic;
using System.Linq;
using CommunityPortal.Data;

namespace CommunityPortal.Models.Services
{

    public class NewsPostService : INewsPostService
    {
        private readonly ApplicationDbContext _appDbContext;

        public NewsPostService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public NewsPost Add(NewsPost newsPost)
        {
            NewsPost newNewsPost = new NewsPost();
            newNewsPost = newsPost;
            _appDbContext.NewsPosts.Add(newNewsPost);
            _appDbContext.SaveChanges();
            return newNewsPost;
        }

        public NewsPost GetById(int newsPostId)
        {
            return _appDbContext.NewsPosts.Where(np => np.NewsPostId == newsPostId).SingleOrDefault();
        }

        public List<NewsPost> GetList()
        {
            return _appDbContext.NewsPosts.ToList();
        }

        public NewsPost Update(NewsPost newsPost)
        {
            int id = newsPost.NewsPostId;
            NewsPost newsPostToUpdate = GetById(id);
            if (newsPostToUpdate == null)
                return null;

            newsPostToUpdate = newsPost;
            newsPostToUpdate.NewsPostId = id;
            _appDbContext.NewsPosts.Update(newsPostToUpdate);
            _appDbContext.SaveChanges();
            return newsPostToUpdate;
        }

        public List<NewsPost> SearchOR(string searchString)
        {
            List<NewsPost> newsPosts = _appDbContext.NewsPosts.ToList();
            List<NewsPost> matches = new List<NewsPost>();
            if (string.IsNullOrWhiteSpace(searchString))
                return newsPosts;

            string[] words = searchString.Split(" ");

            foreach (var post in newsPosts)
            {
                foreach(var word in words)
                {
                    if (string.IsNullOrWhiteSpace(word.Trim()))
                        continue;
                    if ((post.Heading.ToLower().Contains(word.Trim().ToLower())) 
                    || (post.Information.ToLower().Contains(word.Trim().ToLower()))
                    || (post.Tag.ToLower().Contains(word.Trim().ToLower())))
                    {
                        matches.Add(post);
                        break;
                    }   
                }
            }        
            return matches;
        }

        public List<NewsPost> SearchAND(string searchString)
        {
            List<NewsPost> newsPosts = _appDbContext.NewsPosts.ToList();
            List<NewsPost> matches = new List<NewsPost>();
            if (string.IsNullOrWhiteSpace(searchString))
                return newsPosts;//

            string[] words = searchString.Split(" ");

            foreach (var post in newsPosts)
            {
                int wordCount = 0;
                int matchCount = 0;
                foreach(var word in words)
                {
                    if (string.IsNullOrWhiteSpace(word.Trim()))
                        continue;
                    wordCount++;
                    if ((post.Heading.ToLower().Contains(word.Trim().ToLower())) 
                    || (post.Information.ToLower().Contains(word.Trim().ToLower()))
                    || (post.Tag.ToLower().Contains(word.Trim().ToLower())))
                        matchCount++;
                }
                if ((wordCount > 0) && (wordCount == matchCount))
                    matches.Add(post);
            }        
            return matches;
        }

        public bool Delete(int newsPostId)
        {
            NewsPost newsPostToDelete = GetById(newsPostId);
            if (newsPostToDelete == null)
                return false;

            _appDbContext.NewsPosts.Remove(newsPostToDelete);
            _appDbContext.SaveChanges();
            return true;
        }
    }

}