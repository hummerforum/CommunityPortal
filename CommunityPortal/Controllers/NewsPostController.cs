using System;
using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            return JsonConvert.SerializeObject(NewsPosts);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            NewsPost newsPost = _newsPostService.GetById(id);
            return JsonConvert.SerializeObject(newsPost);
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        public string GetByCategoryId(int categoryId)
        {
            List<NewsPost> NewsPosts = _newsPostService.GetListByCategoryId(categoryId);
            return JsonConvert.SerializeObject(NewsPosts);
        }

        [HttpGet("GetByDate/{date}")]
        public string GetByDate(DateTime date)
        {
            List<NewsPost> NewsPosts = _newsPostService.GetListByDate(date);
            return JsonConvert.SerializeObject(NewsPosts);
        }

        [HttpGet("RSS")]
        public string GetRSS()
        {
            return _newsPostService.GetRSS();
        }

        [HttpGet("GetRSSByCategoryId/{categoryId}")]
        public string GetRSSByCategoryId(int categoryId)
        {
            return _newsPostService.GetRSSByCategoryId(categoryId);
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