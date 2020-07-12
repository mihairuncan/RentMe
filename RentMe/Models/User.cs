using Microsoft.AspNetCore.Identity;
using System;

namespace RentMe.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public User()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
