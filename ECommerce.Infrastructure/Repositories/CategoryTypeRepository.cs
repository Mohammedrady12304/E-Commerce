using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce1.ECommerce.Infrastructure.Data;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryTypeRepository:IBaseRepository<CategoryType>
    {
        private readonly ApplicationDbContext _context;

        public CategoryTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryType>> GetAllAsync()
        {
            return await _context.CategoryTypes.Include(x => x.Category).ToListAsync();
        }

        public async Task<CategoryType> GetByIdAsync(int id)
        {
            var categoryType = await _context.CategoryTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
            return categoryType == null ? null! : categoryType;
        }

        public async Task<bool> AddAsync(CategoryType entity)
        {
            await _context.CategoryTypes.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CategoryType entity)
        {
            var categoryType = await GetByIdAsync(entity.Id);
            if (categoryType != null)
            {
                categoryType.Name = entity.Name;
                categoryType.Category = entity.Category;
                _context.CategoryTypes.Update(categoryType);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoryType = await GetByIdAsync(id);
            if (categoryType != null)
            {
                _context.CategoryTypes.Remove(categoryType);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
