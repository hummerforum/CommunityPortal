using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionPostsController : Controller
    {
        private readonly IDiscussionPostsService discussionPostsService;

        public DiscussionPostsController(IDiscussionPostsService discussionPostsService)
        {
            this.discussionPostsService = discussionPostsService;
        }

        #region Read

        [HttpGet]
        public string GetDiscussionPosts()
        {
            List<DiscussionPost> discussionPosts = this.discussionPostsService.Read();

            return JsonConvert.SerializeObject(discussionPosts);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            DiscussionPost discussionPost = this.discussionPostsService.FindDiscussionPost(id);
            return JsonConvert.SerializeObject(discussionPost);
        }

        #endregion

        [HttpPost]
        public void Post([FromBody] DiscussionPost discussionPost)
        {
            this.discussionPostsService.CreateDiscussionPost(discussionPost);
        }

        [HttpPut]
        public void Put([FromBody] DiscussionPost discussionPost)
        {
            this.discussionPostsService.UpdateDiscussionPost(discussionPost);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.discussionPostsService.DeleteDiscussionPost(id);
        }

    }
}
