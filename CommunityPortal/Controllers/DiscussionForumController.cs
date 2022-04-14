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
    public class DiscussionForumController : Controller
    {
        private readonly IDiscussionForumService discussionForumService;

        public DiscussionForumController(IDiscussionForumService discussionForumService)
        {
            this.discussionForumService = discussionForumService;
        }
        
        [HttpGet("Overview")]
        public string Overview()
        {

            var discussionForums = discussionForumService.List(this.User.IsInRole("Admin"));
            return JsonConvert.SerializeObject(discussionForums);
        }
        
        
        [HttpGet("Forum/{forumId:int}")]
        public string Forum(int forumId)
        {
            var discussionTopics = discussionForumService.Forum(forumId);
            return JsonConvert.SerializeObject(discussionTopics);
        }
        
        [HttpGet("Topic/{topicId:int}")]
        public string Topic(int topicId)
        {
            var discussionTopic = discussionForumService.Topic(topicId);
            return JsonConvert.SerializeObject(discussionTopic);
        }
    }
}
