using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Data;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class UserService : IUserService
    {
        
        private readonly IRepository repo;

        public UserService(IRepository repo)
        {
           
            this.repo = repo;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var user = await repo.GetAll<User>().AnyAsync(u => u.Email == email);

            return user;
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            var user = await repo.GetAll<User>().AnyAsync(u => u.PhoneNumber == phone);

            return user;
        }

        public async Task<bool> IsCustomerAsync(string userId)
        {
            bool isCustomer = await repo.GetAll<Customer>().AnyAsync(c => c.IsActive && c.UserId == userId);

            return isCustomer;
        }

        public async Task<bool> IsExistsByIdAsync(string id)
        {
            var isExists = await repo.GetAll<User>()
                .AnyAsync(u => u.Id == id);

            return isExists;

        }
    }
}
