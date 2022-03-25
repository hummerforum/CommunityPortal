using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [Route("api/[controller]")]
    public class PrivateMessageController : ControllerBase
    {
        private readonly PrivateMessageService _privateMessageService;

        public PrivateMessageController(PrivateMessageService privateMessageService)
        {
            _privateMessageService = privateMessageService;
        }

        [HttpGet]
        public string Get()
        {
            List<PrivateMessage> PrivateMessages = _privateMessageService.GetList();
            string JsonData = JsonSerializer.Serialize(PrivateMessages);
            return JsonData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            PrivateMessage privateMessage = _privateMessageService.GetById(id);
            string JsonData = JsonSerializer.Serialize(privateMessage);
            return JsonData;
        }

        [HttpPost]
        public void Post([FromBody] PrivateMessage privateMessage)
        {
            _privateMessageService.Add(privateMessage);
        }

        [HttpPut]
        public void Put([FromBody] PrivateMessage privateMessage)
        {
            _privateMessageService.Update(privateMessage);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _privateMessageService.Delete(id);
        }
    }

}