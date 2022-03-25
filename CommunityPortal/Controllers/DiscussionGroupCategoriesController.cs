using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{
    [Route("api/[controller]")]
    public class DiscussionGroupCategoriesController : Controller
    {
        private readonly IDiscussionGroupCategoriesService discussionGroupCategoriesService;

        public DiscussionGroupCategoriesController(IDiscussionGroupCategoriesService discussionGroupCategoriesService)
        {
            this.discussionGroupCategoriesService = discussionGroupCategoriesService;
        }

        #region Read

        [HttpGet("GetDiscussionGroupCategories")]
        public string GetDiscussionGroupCategories()
        {
            List<DiscussionGroupCategory> discussionGroupCategories = this.discussionGroupCategoriesService.Read();

            return JsonConvert.SerializeObject(discussionGroupCategories);
        }

        #endregion
    }
}
