using System.Collections.Generic;
using System.Linq;
using CommunityPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityPortal.Models.Services
{

    public class PrivateMessagesService : IPrivateMessagesService
    {
        private readonly ApplicationDbContext appDbContext;

        public PrivateMessagesService(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public ReceivedPrivateMessage CreateReceivedPrivateMessage(ReceivedPrivateMessage receivedPrivateMessage)
        {
            this.appDbContext.ReceivedPrivateMessages.Add(receivedPrivateMessage);
            this.appDbContext.SaveChanges();
            return receivedPrivateMessage;
        }

        public SentPrivateMessage CreateSentPrivateMessage(SentPrivateMessage sentPrivateMessage)
        {
            this.appDbContext.SentPrivateMessages.Add(sentPrivateMessage);
            this.appDbContext.SaveChanges();
            return sentPrivateMessage;
        }

        public ReceivedPrivateMessage GetReceivedPrivateMessageById(int receivedPrivateMessageId)
        {
            return this.appDbContext.ReceivedPrivateMessages.Where(pm => pm.Id == receivedPrivateMessageId).SingleOrDefault();
        }

        public SentPrivateMessage GetSentPrivateMessageById(int sentPrivateMessageId)
        {
            return this.appDbContext.SentPrivateMessages.Where(pm => pm.Id == sentPrivateMessageId).SingleOrDefault();
        }

        public List<ReceivedPrivateMessage> GetReceivedPrivateMessages(string communityUserId)
        {
            return this.appDbContext.ReceivedPrivateMessages
                .Where(receivedPrivateMessage => receivedPrivateMessage.ReceiverId == communityUserId)
                .ToList();
        }

        public List<SentPrivateMessage> GetSentPrivateMessages(string communityUserId)
        {
            return this.appDbContext.SentPrivateMessages
                .Where(sentPrivateMessage => sentPrivateMessage.SenderId == communityUserId)
                .ToList();
        }

        public bool UpdateReceivedPrivateMessage(ReceivedPrivateMessage receivedPrivateMessageToUpdate)
        {
            if (GetReceivedPrivateMessageById(receivedPrivateMessageToUpdate.Id) == null)
                return false;

            this.appDbContext.ReceivedPrivateMessages.Update(receivedPrivateMessageToUpdate);
            this.appDbContext.SaveChanges();
            return true;
        }

        public bool DeleteReceivedPrivateMessage(int receivedPrivateMessageId)
        {
            ReceivedPrivateMessage receivedPrivateMessageToDelete = GetReceivedPrivateMessageById(receivedPrivateMessageId);
            if (receivedPrivateMessageToDelete == null)
                return false;

            this.appDbContext.ReceivedPrivateMessages.Remove(receivedPrivateMessageToDelete);
            this.appDbContext.SaveChanges();
            return true;
        }

        public bool DeleteSentPrivateMessage(int sentPrivateMessageId)
        {
            SentPrivateMessage sentPrivateMessageToDelete = GetSentPrivateMessageById(sentPrivateMessageId);
            if (sentPrivateMessageToDelete == null)
                return false;

            this.appDbContext.SentPrivateMessages.Remove(sentPrivateMessageToDelete);
            this.appDbContext.SaveChanges();
            return true;
        }
    }

}