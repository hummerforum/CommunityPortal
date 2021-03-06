using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Models.React
{
    public class CreatePrivateMessage
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverUserName { get; set; }

        public string SenderId { get; set; }

        public string SenderUserName { get; set; }
    }
}
