using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RentMe.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

    }
}
