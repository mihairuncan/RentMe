using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentMe.ViewModels
{
    public class UserForRegister
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify passsword between 4 and 8 characters")]
        public string Password { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
