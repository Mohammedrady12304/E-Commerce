using E_Commerce1.ECommerce.Infrastructure.Data;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryTypeRepository : ICategoryTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryTypeRepository(ApplicationDbContext context)
        {
            _context = context;  
        }
        public Task AddAsync(CategoryType entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryType>> GetAllAsync()
        {
            return await _context.CategoryTypes.Include(ct => ct.products).ToListAsync();
        }

        public Task<CategoryType> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryType> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync( CategoryType New)
        {
            throw new NotImplementedException();
        }
    }
}
