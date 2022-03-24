using CommunityPortal.Models;
using CommunityPortal.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [Route("api/[controller]")]
    public class NewsPostController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public NewsPostController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string Get()
        {
            List<NewsPost> NewsPosts = _dbContext.NewsPosts.ToList();
            string JsonData = JsonSerializer.Serialize(NewsPosts);
            return JsonData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<NewsPost> NewsPosts = _dbContext.NewsPosts.ToList();
            NewsPost newsPost = NewsPosts.Where(np => np.NewsPostId == id).SingleOrDefault();
            string JsonData = JsonSerializer.Serialize(newsPost);
            return JsonData;
        }
    }

}