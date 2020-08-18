using System;

namespace RentMe.ViewModels
{
    public class MessageForList
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
