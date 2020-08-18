using System;

namespace RentMe.ViewModels
{
    public class MessageForCreation
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }
        public MessageForCreation()
        {
            MessageSent = DateTime.Now;
        }
    }
}
