﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Shoes;
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
    public class ShoeService : IShoeService
    {

        private readonly IRepository repo;
        private readonly IImageService imageService;
        private readonly IHttpContextAccessor accessor;

        public ShoeService(IRepository repo, IImageService imageService, IHttpContextAccessor accessor)
        {
            this.repo = repo;
            this.imageService = imageService;
            this.accessor = accessor;
        }
        public async Task<AllShoesFilteredAndPaged> GetAllShoesFilteredAndPagedAsync(ShoesQueryModel model)
        {
            IQueryable<Shoe> shoesQuery = repo.GetAll<Shoe>()
                .Include(s => s.Category)
                .Include(s => s.SizeShoe)
                .Where(s => s.IsActive);


            if (!string.IsNullOrEmpty(model.Category))
            {
                shoesQuery = shoesQuery.Where(sh => sh.Category.Name == model.Category);
            }

            if (!string.IsNullOrEmpty(model.SearchString))
            {
                string wildCard = $"%{model.SearchString.ToLower()}%";

                shoesQuery = shoesQuery
                    .Where(d => EF.Functions.Like(d.Name, wildCard) ||
                                EF.Functions.Like(d.Description, wildCard));

            }

            IEnumerable<ShoeViewModel> shoeModel = await shoesQuery
                .Where(d => d.IsActive)
                .Skip((model.CurrentPage - 1) * model.ShoesPerPage)
                .Take(model.ShoesPerPage)
                .Select(s => new ShoeViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    ShoePictureUrl = s.ShoeUrlImage,
                    Description = s.Description,

                })
                .ToListAsync();


            return new AllShoesFilteredAndPaged()
            {
                Shoes = shoeModel,
                TotalShoes = shoeModel.Count()
            };
        }

        public async Task<string> AddAsync(ShoeFormModel model)
        {
            var shoe = new Shoe()
            {
                CategoryId = model.CategoryId!,
                Name = model.Name,
                Description = model.Description,
                Price = Decimal.Parse(model.Price),

            };

            if (model.ShoeUrlImage != null)
            {
                shoe.ShoeUrlImage = await imageService.UploadImageToShoe(model.ShoeUrlImage!, "ShoeManiaProject", shoe);
            }

            await repo.AddAsync<Shoe>(shoe);
            await repo.SaveChangesAsync();

            return shoe.Id;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await repo.GetAll<Shoe>().AnyAsync(sho => sho.Id == id);
        }
    }
}