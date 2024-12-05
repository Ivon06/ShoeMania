using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class DeliveryOfficeService : IDeliveryOfficeService
    {
        private readonly IRepository repo;

        public DeliveryOfficeService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<DeliveryOffice>> GetDeliveryOfficeListAsync()
        {
            return await repo.GetAll<DeliveryOffice>().ToListAsync();
        }
    }
}
