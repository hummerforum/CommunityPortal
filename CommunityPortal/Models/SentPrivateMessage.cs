using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class SentPrivateMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string ReceiverUserName { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string SenderUserName { get; set; }

        [Required]
        public CommunityUser Sender { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }
    }

}