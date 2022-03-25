using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{

    public interface IPrivateMessageService
    {
        public PrivateMessage Add(PrivateMessage privateMessage);
        public PrivateMessage GetById(int privateMessageId);
        public List<PrivateMessage> GetList();
        public PrivateMessage Update(PrivateMessage privateMessage);
        public bool Delete(int privateMessageId);
    }

}