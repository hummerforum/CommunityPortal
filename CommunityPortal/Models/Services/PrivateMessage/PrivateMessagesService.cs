using System.Collections.Generic;
using System.Linq;
using CommunityPortal.Data;

namespace CommunityPortal.Models.Services
{

    public class PrivateMessagesService : IPrivateMessagesService
    {
        private readonly ApplicationDbContext _appDbContext;

        public PrivateMessagesService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public PrivateMessage Add(PrivateMessage privateMessage)
        {
            _appDbContext.PrivateMessages.Add(privateMessage);
            _appDbContext.SaveChanges();
            return privateMessage;
        }

        public PrivateMessage GetById(int privateMessageId)
        {
            return _appDbContext.PrivateMessages.Where(pm => pm.PrivateMessageId == privateMessageId).SingleOrDefault();
        }

        public List<PrivateMessage> GetList()
        {
            return _appDbContext.PrivateMessages.ToList();
        }

        public PrivateMessage Update(PrivateMessage privateMessage)
        {
            int id = privateMessage.PrivateMessageId;
            PrivateMessage privateMessageToUpdate = GetById(id);
            if (privateMessageToUpdate == null)
                return null;

            privateMessageToUpdate = privateMessage;
            privateMessageToUpdate.PrivateMessageId = id;
            _appDbContext.PrivateMessages.Update(privateMessageToUpdate);
            _appDbContext.SaveChanges();
            return privateMessageToUpdate;
        }

        public bool Delete(int privateMessageId)
        {
            PrivateMessage privateMessageToDelete = GetById(privateMessageId);
            if (privateMessageToDelete == null)
                return false;

            _appDbContext.PrivateMessages.Remove(privateMessageToDelete);
            _appDbContext.SaveChanges();
            return true;
        }
    }

}