using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.ViewModels
{
    public class UserWithRoles
    {
        public string Id{ get; set; }
        public string UserName { get; set; }
        public List<string> Roles{ get; set; }
    }
}
