using System.Collections.Generic;
using System.Linq;
using CommunityPortal.Data;

namespace CommunityPortal.Models.Services
{

    public class PrivateMessageService : IPrivateMessageService
    {
        private readonly ApplicationDbContext _appDbContext;

        public PrivateMessageService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public PrivateMessage Add(PrivateMessage privateMessage)
        {
            PrivateMessage newPrivateMessage = new PrivateMessage();
            newPrivateMessage = privateMessage;
            _appDbContext.PrivateMessages.Add(newPrivateMessage);
            _appDbContext.SaveChanges();
            return newPrivateMessage;
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