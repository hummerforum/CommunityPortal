using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NewsPostController : ControllerBase
    {
        private readonly INewsPostService _newsPostService;

        public NewsPostController(INewsPostService newsPostService)
        {
            _newsPostService = newsPostService;
        }

        [HttpGet]
        public string Get()
        {
            List<NewsPost> NewsPosts = _newsPostService.GetList();
            return JsonSerializer.Serialize(NewsPosts);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            NewsPost newsPost = _newsPostService.GetById(id);
            return JsonSerializer.Serialize(newsPost);
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        public string GetByCategoryId(int categoryId)
        {
            List<NewsPost> NewsPosts = _newsPostService.GetListByCategoryId(categoryId);
            return JsonSerializer.Serialize(NewsPosts);
        }

        [HttpGet("rss")]
        public string GetRSS()
        {
            return _newsPostService.GetRSS();
        }

        [HttpPost]
        public void Post([FromBody] NewsPost newsPost)
        {
            _newsPostService.Add(newsPost);
        }

        [HttpPut]
        public void Put([FromBody] NewsPost newsPost)
        {
            _newsPostService.Update(newsPost);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _newsPostService.Delete(id);
        }
    }

}