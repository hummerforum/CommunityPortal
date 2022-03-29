using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{

    public class PrivateMessage
    {
        [Key]
        public int PrivateMessageId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Sender { get; set; }

        public CommunityUser SenderCommunityUser { get; set; }

        [Required]
        public string Receiver { get; set; }

        public CommunityUser ReceiverCommunityUser { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }
    }

}