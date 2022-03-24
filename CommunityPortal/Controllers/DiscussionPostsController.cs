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
    [Route("api/[controller]")]
    public class DiscussionPostsController : Controller
    {
        private readonly IDiscussionPostsService discussionPostsService;

        public DiscussionPostsController(IDiscussionPostsService discussionPostsService)
        {
            this.discussionPostsService = discussionPostsService;
        }

        #region Read

        [HttpGet("GetDiscussionPosts")]
        public string GetDiscussionPosts()
        {
            List<DiscussionPost> discussionPosts = this.discussionPostsService.Read();

            return JsonConvert.SerializeObject(discussionPosts);
        }

        #endregion
    }
}
