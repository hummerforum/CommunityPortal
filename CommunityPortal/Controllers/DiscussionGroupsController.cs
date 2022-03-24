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
    public class DiscussionGroupsController : Controller
    {
        private readonly IDiscussionGroupsService discussionGroupsService;

        public DiscussionGroupsController(IDiscussionGroupsService discussionGroupsService)
        {
            this.discussionGroupsService = discussionGroupsService;
        }

        #region Read

        [HttpGet("GetDiscussionGroups")]
        public string GetDiscussionGroups()
        {
            List<DiscussionGroup> discussionGroups = this.discussionGroupsService.Read();

            return JsonConvert.SerializeObject(discussionGroups);
        }

        #endregion
    }
}
