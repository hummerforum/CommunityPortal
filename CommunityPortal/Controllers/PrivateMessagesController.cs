using CommunityPortal.Models;
using CommunityPortal.Models.React;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CommunityPortal.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User, Moderator, Admin")]
    public class PrivateMessagesController : Controller
    {
        private readonly IUserService userService;
        private readonly IPrivateMessagesService privateMessagesService;

        public PrivateMessagesController(IUserService userService, IPrivateMessagesService privateMessagesService)
        {
            this.userService = userService;
            this.privateMessagesService = privateMessagesService;
        }

        [HttpPost("CreatePrivateMessage")]
        public IActionResult CreatePrivateMessage([FromBody] CreatePrivateMessage createPrivateMessage)
        {
            ReceivedPrivateMessage receivedPrivateMessage = new ReceivedPrivateMessage()
            {
                Subject = createPrivateMessage.Subject,
                Message = createPrivateMessage.Message,
                ReceiverId = createPrivateMessage.ReceiverId,
                ReceiverUserName = createPrivateMessage.ReceiverUserName,
                SenderId = createPrivateMessage.SenderId,
                SenderUserName = createPrivateMessage.SenderUserName,
                TimeReceived = DateTime.Now
            };

            this.privateMessagesService.CreateReceivedPrivateMessage(receivedPrivateMessage);


            SentPrivateMessage sentPrivateMessage = new SentPrivateMessage()
            {
                Subject = createPrivateMessage.Subject,
                Message = createPrivateMessage.Message,
                ReceiverId = createPrivateMessage.ReceiverId,
                ReceiverUserName = createPrivateMessage.ReceiverUserName,
                SenderId = createPrivateMessage.SenderId,
                SenderUserName = createPrivateMessage.SenderUserName,
                TimeSent = DateTime.Now
            };

            this.privateMessagesService.CreateSentPrivateMessage(sentPrivateMessage);

            return StatusCode(201);
        }

        [HttpGet("GetReceivedPrivateMessages/{communityUserId}")]
        public string GetReceivedPrivateMessages(string communityUserId)
        {
            List<ReceivedPrivateMessage> receivedPrivateMessages = this.privateMessagesService.GetReceivedPrivateMessages(communityUserId);

            List<GetPrivateMessage> receivedPrivateMessagesToReturn = new List<GetPrivateMessage>();

            foreach (var receivedPrivateMessage in receivedPrivateMessages)
            {
                receivedPrivateMessagesToReturn.Add(
                    new GetPrivateMessage()
                    {
                        Id = receivedPrivateMessage.Id,
                        Subject = receivedPrivateMessage.Subject,
                        Message = receivedPrivateMessage.Message,
                        ReceiverId = receivedPrivateMessage.ReceiverId,
                        ReceiverUserName = receivedPrivateMessage.ReceiverUserName,
                        SenderId = receivedPrivateMessage.SenderId,
                        SenderUserName = receivedPrivateMessage.SenderUserName,
                        TimeSent = receivedPrivateMessage.TimeReceived.ToString("yyyy-MM-dd HH:mm")
                    }
                    );
            }


            return JsonConvert.SerializeObject(receivedPrivateMessagesToReturn);
        }

        [HttpGet("GetSentPrivateMessages/{communityUserId}")]
        public string GetSentPrivateMessages(string communityUserId)
        {
            List<SentPrivateMessage> sentPrivateMessages = this.privateMessagesService.GetSentPrivateMessages(communityUserId);

            List<GetPrivateMessage> sentPrivateMessagesToReturn = new List<GetPrivateMessage>();

            foreach (var receivedPrivateMessage in sentPrivateMessages)
            {
                sentPrivateMessagesToReturn.Add(
                    new GetPrivateMessage()
                    {
                        Id = receivedPrivateMessage.Id,
                        Subject = receivedPrivateMessage.Subject,
                        Message = receivedPrivateMessage.Message,
                        ReceiverId = receivedPrivateMessage.ReceiverId,
                        ReceiverUserName = receivedPrivateMessage.ReceiverUserName,
                        SenderId = receivedPrivateMessage.SenderId,
                        SenderUserName = receivedPrivateMessage.SenderUserName,
                        TimeSent = receivedPrivateMessage.TimeSent.ToString("yyyy-MM-dd HH:mm")
                    }
                    );
            }


            return JsonConvert.SerializeObject(sentPrivateMessagesToReturn);
        }

        [HttpGet("GetAllUsers")]
        public string GetAllUsers()
        {
            List<CommunityUser> communityUsers = this.userService.GetAllUsers();
            List<GetAllUsers> usersToReturn = new List<GetAllUsers>();

            foreach (var user in communityUsers)
            {
                usersToReturn.Add(
                    new GetAllUsers()
                    {
                        Id = user.Id,
                        UserName = user.UserName
                    }
                    );
            }


            return JsonConvert.SerializeObject(usersToReturn);
        }

        [HttpGet("DeleteReceivedPrivateMessage/{receivedPrivateMessageId}")]
        public IActionResult DeleteReceivedPrivateMessage(int receivedPrivateMessageId)
        {
            bool wasReceivedPrivateMessageDeleted = this.privateMessagesService.DeleteReceivedPrivateMessage(receivedPrivateMessageId);

            if (wasReceivedPrivateMessageDeleted)
            {
                return StatusCode(204);
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpGet("DeleteSentPrivateMessage/{sentPrivateMessageId}")]
        public IActionResult DeleteSentPrivateMessage(int sentPrivateMessageId)
        {
            bool wasSentPrivateMessageDeleted = this.privateMessagesService.DeleteSentPrivateMessage(sentPrivateMessageId);

            if (wasSentPrivateMessageDeleted)
            {
                return StatusCode(204);
            }
            else
            {
                return StatusCode(400);
            }
        }
    }

}