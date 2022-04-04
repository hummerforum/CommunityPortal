using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionGroupMembershipsController : Controller
    {
        private readonly IDiscussionGroupMembershipsService discussionGroupMembershipsService;

        public DiscussionGroupMembershipsController(IDiscussionGroupMembershipsService discussionGroupMembershipsService)
        {
            this.discussionGroupMembershipsService = discussionGroupMembershipsService;
        }

        #region Read

        [HttpGet]
        public string GetDiscussionGroupMemberships()
        {
            List<DiscussionGroupMembership> discussionGroupMemberships = this.discussionGroupMembershipsService.Read();

            return JsonConvert.SerializeObject(discussionGroupMemberships);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            DiscussionGroupMembership discussionGroupMemberships = this.discussionGroupMembershipsService.FindDiscussionGroupMembership(id);
            return JsonConvert.SerializeObject(discussionGroupMemberships);
        }

        #endregion

        [HttpPost]
        public void Post([FromBody] DiscussionGroupMembership discussionGroupMembership)
        {
            this.discussionGroupMembershipsService.CreateDiscussionGroupMembership(discussionGroupMembership);
        }

        [HttpPut]
        public void Put([FromBody] DiscussionGroupMembership discussionGroupMembership)
        {
            this.discussionGroupMembershipsService.UpdateDiscussionGroupMembership(discussionGroupMembership);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.discussionGroupMembershipsService.DeleteDiscussionGroupMembership(id);
        }

    }
}
