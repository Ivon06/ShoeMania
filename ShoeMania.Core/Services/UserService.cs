using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ShoeManiaDbContext context;

        public UserService(ShoeManiaDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var user = await context.Users.AnyAsync(u => u.Email == email);

            return user;
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            var user = await context.Users.AnyAsync(u => u.PhoneNumber == phone);

            return user;
        }

        public async Task<bool> IsCustomerAsync(string userId)
        {
            bool isCustomer = await context.Customers.AnyAsync(c => c.IsActive && c.UserId == userId);

            return isCustomer;
        }

        public async Task<bool> IsExistsByIdAsync(string id)
        {
            var isExists = await context.Users
                .AnyAsync(u => u.Id == id);

            return isExists;

        }
    }
}
