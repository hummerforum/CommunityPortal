using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [Route("api/[controller]")]
    public class NewsPostController : Controller
    {
        private readonly NewsPostService _newsPostService;

        public NewsPostController(NewsPostService newsPostService)
        {
            _newsPostService = newsPostService;
        }

        [HttpGet]
        public string Get()
        {
            List<NewsPost> NewsPosts = _newsPostService.GetList();
            string JsonData = JsonSerializer.Serialize(NewsPosts);
            return JsonData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            NewsPost newsPost = _newsPostService.GetById(id);
            string JsonData = JsonSerializer.Serialize(newsPost);
            return JsonData;
        }

        /*[HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }

}