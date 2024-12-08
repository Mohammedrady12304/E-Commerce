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

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Category>> GetAllAsync(){
            return await _context.Categories.Include(c => c.CategoryTypes).ToListAsync();
        }
       public Task<Category> GetByIdAsync(string id) {
            throw new NotImplementedException();
        }
       public Task<Category> GetByIdAsync(int id) {
            throw new NotImplementedException();

        }
        public Task AddAsync(Category entity) { throw new NotImplementedException(); }
        public Task UpdateAsync(Category New) { throw new NotImplementedException(); }
        public Task DeleteAsync(string id) { throw new NotImplementedException(); }
        public Task<bool> DeleteAsync(int id) { throw new NotImplementedException(); }
    }
}
