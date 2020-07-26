using RentMe.Helpers;
using RentMe.Models;
using RentMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Services
{
    public interface IUserService
    {
        public Task<PagedList<User>> GetUsersWithRoles( UserParams userParams);
        public Task<IEnumerable<string>> EditRoles(string userName, RolesForEdit rolesForEdit);
    }
}
