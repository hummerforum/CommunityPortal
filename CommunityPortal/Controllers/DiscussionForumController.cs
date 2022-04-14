using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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
            var discussionForums = discussionForumService.Overview(this.User.IsInRole("Admin"));
            return JsonConvert.SerializeObject(discussionForums);
        }


        [HttpGet("Topics/{forumId:int}")]
        public string Topics(int forumId)
        {
            var discussionTopics = discussionForumService.Topics(forumId);
            return JsonConvert.SerializeObject(discussionTopics);
        }

        [HttpGet("Topic/{topicId:int}")]
        public string Topic(int topicId)
        {
            var discussionTopic = discussionForumService.Topic(topicId);
            return JsonConvert.SerializeObject(discussionTopic);
        }

        [HttpGet("Replies/{topicId:int}")]
        public string Replies(int topicId)
        {
            var discussionTopic = discussionForumService.Replies(topicId);
            return JsonConvert.SerializeObject(discussionTopic);
        }

        [Authorize]
        [HttpPost("TopicCreate/{forumId:int}")]
        public string CreateTopic(int forumId, [FromForm] string subject, [FromForm] string content)
        {
            string userId = this.User.GetSubjectId();
            if (content == null && subject == null) return "500";
            string discussionTopic = discussionForumService.CreateTopic(userId, forumId, subject, content);
            return JsonConvert.SerializeObject(discussionTopic);
        }

        [Authorize]
        [HttpPost("ReplyCreate/{topicId:int}")]
        public string CreateReply(int topicId, [FromForm] string content)
        {
            string userId = this.User.GetSubjectId();
            if (content == null) return "500";
            string discussionTopic = discussionForumService.CreateReply(userId, topicId, content);
            return JsonConvert.SerializeObject(discussionTopic);
        }
    }
}