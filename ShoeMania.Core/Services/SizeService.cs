using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Sizes;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class SizeService : ISizeService
    {
        private readonly IRepository repo;


        public SizeService(IRepository repo)
        {
            
            this.repo = repo;
        }

        public async Task AddSizesToShoeAsync(List<int> sizesIds, string shoeId)
        {
            var shoeSizes = new List<SizeShoe>();

            foreach (var sizeId in sizesIds)
            {
                shoeSizes.Add(new SizeShoe()
                {
                    ShoeId = shoeId,
                    SizeId = sizeId
                });
            };

            await repo.AddRangeAsync<SizeShoe>(shoeSizes);

            await repo.SaveChangesAsync();


        }

        public async Task<List<SizeViewModel>> GetAllSizesAsync()
        {
            var sizes = await repo.GetAll<Size>()
                .Select(s => new SizeViewModel()
                {
                    Id = s.Id,
                    Number = s.Number
                })
                .ToListAsync();

            return sizes;
        }
    }
}
