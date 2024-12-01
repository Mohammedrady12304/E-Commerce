using E_Commerce1.ECommerce.Infrastructure.Data;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task AddAsync(ApplicationUser entity)
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

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public Task<ApplicationUser> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            ApplicationUser applicationUser =await _userManager.FindByIdAsync(id);
            return applicationUser;
        }

        public Task<ApplicationUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync( ApplicationUser New)
        {
            throw new NotImplementedException();
        }
    }
}
