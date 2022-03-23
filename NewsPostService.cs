using System.Collections.Generic;
using CommunityPortal.Models.Repos;

namespace CommunityPortal.Models.Services
{

    public class NewsPostService : INewsPostService
    {
        private readonly INewsPostRepository _newsPostRepository;

        public NewsPostService()
        {
            _newsPostRepository = new NewsPostRepository();
        }

        public NewsPost Add(NewsPost newsPost)
        {
            return _newsPostRepository.Create(newsPost);
        }

        public NewsPost GetById(int newsPostId)
        {
            return _newsPostRepository.Read(newsPostId);
        }

        public List<NewsPost> GetList()
        {
            return _newsPostRepository.Read();
        }

        public List<NewsPost> SearchOR(string searchString)
        {
            List<NewsPost> newsPosts = _newsPostRepository.Read();
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
            List<NewsPost> newsPosts = _newsPostRepository.Read();
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
            return _newsPostRepository.Delete(newsPostId);
        }
    }

}