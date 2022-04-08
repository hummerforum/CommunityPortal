using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{

    public interface IPrivateMessagesService
    {
        public ReceivedPrivateMessage CreateReceivedPrivateMessage(ReceivedPrivateMessage receivedPrivateMessage);
        public SentPrivateMessage CreateSentPrivateMessage(SentPrivateMessage sentPrivateMessage);
        public ReceivedPrivateMessage GetReceivedPrivateMessageById(int receivedPrivateMessageId);
        public SentPrivateMessage GetSentPrivateMessageById(int sentPrivateMessageId);
        public List<ReceivedPrivateMessage> GetReceivedPrivateMessages(string communityUserId);
        public List<SentPrivateMessage> GetSentPrivateMessages(string communityUserId);
        public bool DeleteReceivedPrivateMessage(int receivedPrivateMessageId);
        public bool DeleteSentPrivateMessage(int sentPrivateMessageId);
    }

}