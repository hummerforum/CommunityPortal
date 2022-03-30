using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class PrivateMessagesController : Controller
    {
        private readonly IPrivateMessagesService _privateMessagesService;

        public PrivateMessagesController(IPrivateMessagesService privateMessagesService)
        {
            _privateMessagesService = privateMessagesService;
        }

        [HttpGet]
        public string Get()
        {
            List<PrivateMessage> PrivateMessages = _privateMessagesService.GetList();
            string JsonData = JsonSerializer.Serialize(PrivateMessages);
            return JsonData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            PrivateMessage privateMessage = _privateMessagesService.GetById(id);
            string JsonData = JsonSerializer.Serialize(privateMessage);
            return JsonData;
        }

        [HttpPost]
        public void Post([FromBody] PrivateMessage privateMessage)
        {
            _privateMessagesService.Add(privateMessage);
        }

        [HttpPut]
        public void Put([FromBody] PrivateMessage privateMessage)
        {
            _privateMessagesService.Update(privateMessage);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _privateMessagesService.Delete(id);
        }
    }

}