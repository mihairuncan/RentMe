using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RentMe.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
