using System;
using System.ComponentModel.DataAnnotations;

namespace lb1.Models
{
    public class Message
    {
        public int MessageID { get; set; } // Primary key

        [Required]
        public string MessageContent { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now;

        // Foreign key до Ad
        public int AdID { get; set; }
        public Ad Ad { get; set; }

        // Foreign key до SenderUser
        public int SenderUserID { get; set; }
        public User SenderUser { get; set; }

        // Foreign key до ReceiverUser
        public int ReceiverUserID { get; set; }
        public User ReceiverUser { get; set; }
    }
}
