using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{
    [Route("api/[controller]")]
    public class DiscussionGroupMembershipsController : Controller
    {
        private readonly IDiscussionGroupMembershipsService discussionGroupMembershipsService;

        public DiscussionGroupMembershipsController(IDiscussionGroupMembershipsService discussionGroupMembershipsService)
        {
            this.discussionGroupMembershipsService = discussionGroupMembershipsService;
        }

        #region Read

        [HttpGet("GetDiscussionGroupMemberships")]
        public string GetDiscussionGroupMemberships()
        {
            List<DiscussionGroupMembership> discussionGroupMemberships = this.discussionGroupMembershipsService.Read();

            return JsonConvert.SerializeObject(discussionGroupMemberships);
        }

        #endregion
    }
}
