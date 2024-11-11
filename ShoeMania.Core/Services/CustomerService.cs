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
    public class CustomerService : ICustomerService
    {
        //private readonly ShoeManiaDbContext context;
        private readonly IRepository repo;

        public CustomerService(IRepository repo)
        {
            
            this.repo = repo;
        }

        public async Task Create(string userId)
        {
            var customer = new Customer()
            {
                UserId = userId,
            };

            await repo.AddAsync(customer);
            await repo.SaveChangesAsync();
        }

        public async Task<string?> GetCustomerIdByUserIdAsync(string userId)
        {
            var customer = await repo.GetAll<Customer>()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return customer?.Id;
        }
    }
}
