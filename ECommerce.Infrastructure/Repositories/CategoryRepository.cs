using E_Commerce1.ECommerce.Infrastructure.Data;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.types).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> UpdateAsync(Category entity)
        {
            var category = await GetByIdAsync(entity.Id);
            if(category != null)
            {
                category.Name = entity.Name;
                _context.Categories.Update(category);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
