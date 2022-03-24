using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{
    [Route("api/[controller]")]
    public class SubForumController : Controller
    {
        private readonly ISubForumService subForumService;

        public SubForumController(ISubForumService subForumService)
        {
            this.subForumService = subForumService;
        }

        #region Read

        [HttpGet("GetSubForum")]
        public string GetSubForum()
        {
            List<SubForum> subForum = this.subForumService.Read();

            return JsonConvert.SerializeObject(subForum);
        }

        #endregion
    }
}
