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
    public class DiscussionGroupsController : Controller
    {
        private readonly IDiscussionGroupsService discussionGroupsService;

        public DiscussionGroupsController(IDiscussionGroupsService discussionGroupsService)
        {
            this.discussionGroupsService = discussionGroupsService;
        }

        #region Read

        [HttpGet]
        public string GetDiscussionGroups()
        {
            List<DiscussionGroup> discussionGroups = this.discussionGroupsService.Read();

            return JsonConvert.SerializeObject(discussionGroups);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            DiscussionGroup discussionGroup = this.discussionGroupsService.FindDiscussionGroup(id);
            return JsonConvert.SerializeObject(discussionGroup);
        }

        #endregion

        [HttpPost]
        public void Post([FromBody] DiscussionGroup discussionGroup)
        {
            this.discussionGroupsService.CreateDiscussionGroup(discussionGroup);
        }

        [HttpPut]
        public void Put([FromBody] DiscussionGroup discussionGroup)
        {
            this.discussionGroupsService.UpdateDiscussionGroup(discussionGroup);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.discussionGroupsService.DeleteDiscussionGroup(id);
        }

    }
}
