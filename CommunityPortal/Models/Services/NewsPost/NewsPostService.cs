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

        public List<NewsPost> GetListByCategoryId(int categoryId)
        {
            List<NewsPost> newsPosts = new List<NewsPost>();
            foreach (var newsPost in _appDbContext.NewsPosts)
            {
                if (newsPost.CategoryId == categoryId)
                    newsPosts.Add(newsPost);
            }
            return newsPosts;
        }

        public string GetRSS()
        {
            string URL = "https://localhost:5001/";
            string RSSFeed = "<?xml version=\"1.0\"?>\n" +
                "<rss version=\"0.91\">\n" +
                "  <channel>\n" +
                "    <title>Hummmer Forum</title>\n" +
                $"    <link>{URL}</link>\n" +
                "    <description></description>\n" +
                "    <language>en-us</language>\n" +
                "    <copyright>&copy; 2022, Hummer Forum. All Rights Reserved.</copyright>\n" +
                "    <image>\n" +
                "      <title>Hummer Forum</title>\n" +
                $"      <url>{URL}static/media/lobster.98b40151450656d37be6.jpg</url>\n" +
                $"      <link>{URL}</link>\n" +
                "    </image>\n";
            
            List<NewsPost> newsPosts = _appDbContext.NewsPosts.ToList();
            foreach (var newsPost in newsPosts)
            {
                RSSFeed += "      <item>\n" +
                    $"        <title>{newsPost.Heading}</title>\n" +
                    $"        <link>{URL}{newsPost.NewsPostId}</link>\n" +
                    $"        <quid>{URL}{newsPost.NewsPostId}</guid>\n" +
                    $"        <description>{newsPost.Description}</description>\n" +
                    "      </item>\n";
            }

            RSSFeed += "  </channel>\n" +
                "</rss>";
            return RSSFeed;
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