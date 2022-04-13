using System;
using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{

    public interface INewsPostService
    {
        public NewsPost Add(NewsPost newsPost);
        public NewsPost GetById(int newsPostId);
        public List<NewsPost> GetList();
        public List<NewsPost> GetListByCategoryId(int categoryId);
        public List<NewsPost> GetListByDate(DateTime date);
        public string GetRSS();
        public string GetRSSByCategoryId(int categoryId);
        public NewsPost Update(NewsPost newsPost);
        List<NewsPost> SearchOR(string searchString);
        List<NewsPost> SearchAND(string searchString);
        public bool Delete(int newsPostId);
    }

}