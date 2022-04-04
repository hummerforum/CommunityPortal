using CommunityPortal.Model.Services;
using CommunityPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubForumController : Controller
    {
        private readonly ISubForumService subForumService;

        public SubForumController(ISubForumService subForumService)
        {
            this.subForumService = subForumService;
        }

        #region Read

        [HttpGet]
        public string GetSubForum()
        {
            List<SubForum> subForum = this.subForumService.Read();

            return JsonConvert.SerializeObject(subForum);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            SubForum subForum = this.subForumService.FindSubForum(id);
            return JsonConvert.SerializeObject(subForum);
        }

        #endregion

        [HttpPost]
        public void Post([FromBody] SubForum subForum)
        {
            this.subForumService.CreateSubForum(subForum);
        }

        [HttpPut]
        public void Put([FromBody] SubForum subForum)
        {
            this.subForumService.UpdateSubForum(subForum);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.subForumService.DeleteSubForum(id);
        }

    }
}
