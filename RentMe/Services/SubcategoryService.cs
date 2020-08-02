using Microsoft.EntityFrameworkCore;
using RentMe.Data;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Services
{
    public interface ISubcategoryService
    {
        public Task<Subcategory> GetSubcategoryByName(string subcategoryName);
    }

    public class SubcategoryService : ISubcategoryService
    {
        private readonly DatabaseContext _context;

        public SubcategoryService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Subcategory> GetSubcategoryByName(string subcategoryName)
        {
            return await _context.Subcategories.FirstOrDefaultAsync(s => s.Name == subcategoryName);
        }
    }
}
