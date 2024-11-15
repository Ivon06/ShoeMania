using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Category;
using ShoeMania.Core.ViewModels.Shoes;
using ShoeMania.Core.ViewModels.Sizes;
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

        public async Task<ShoeFormModel> GetShoeForEditAsync(string shoeId)
        {
            var shoe = await repo.GetAll<Shoe>()
                 .Where(sh => sh.Id == shoeId)
                 .Select(sh => new ShoeFormModel()
                 {
                     Name = sh.Name,
                     Description = sh.Description,
                     Price = sh.Price.ToString(),

                 })
                 .FirstOrDefaultAsync();

            if (shoe == null)
            {
                return null;
            }


            shoe!.Sizes = await repo.GetAll<Size>()
                .Select(s => new SizeViewModel()
                {
                    Id = s.Id,
                    Number = s.Number,
                })
                .ToListAsync();

            shoe.Categories = await repo.GetAll<Category>()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return shoe;
        }

        public async Task EditShoeAsync(ShoeFormModel shoe, string shoeId)
        {
            var model = await repo.GetAll<Shoe>()
                .Include(s => s.SizeShoe)
                .FirstOrDefaultAsync(sh => sh.Id == shoeId);

            if (model == null)
            {
                return;
            }

            model.Name = shoe.Name;
            model.Description = shoe.Description;
            model.Price = Decimal.Parse(shoe.Price);

            if (shoe.ShoeUrlImage != null)
            {
                model.ShoeUrlImage = await imageService.UploadImageToShoe(shoe.ShoeUrlImage!, "FootTrapProject", model);
            }

            if (shoe.CategoryId != null)
            {
                model.CategoryId = shoe.CategoryId;
            }

            List<SizeShoe> sizes = new List<SizeShoe>();

            if (shoe.SizeIds.Count() != 0)
            {
                foreach (var size in shoe.SizeIds)
                {
                    SizeShoe sz = new SizeShoe()
                    {
                        ShoeId = shoeId,
                        SizeId = size
                    };

                    sizes.Add(sz);
                }

                repo.RemoveRange(model.SizeShoe);
            }

            await repo.AddRangeAsync(sizes);

            await repo.SaveChangesAsync();

        }

        public async Task<PreDeleteShoeViewModel> GetShoeForDeleteAsync(string shoeId)
        {
            var model = await repo.GetAll<Shoe>()
                .Include(sh => sh.Category)
                .FirstOrDefaultAsync(sh => sh.Id == shoeId);

            if (model == null)
            {
                return null;
            }

            PreDeleteShoeViewModel shoe = new PreDeleteShoeViewModel()
            {
                Id = shoeId,
                Name = model.Name,
                Category = model.Category.Name,
                Description = model.Description,
                Price = model.Price,
                ShoePictureUrl = model.ShoeUrlImage
            };

            return shoe;

        }

        public async Task DeleteShoeAsync(string shoeId)
        {
            var shoe = await repo.GetAll<Shoe>()
                .FirstOrDefaultAsync(sh => sh.Id == shoeId);

            if (shoe == null)
            {
                return;
            }

            shoe.IsActive = false;

            await repo.SaveChangesAsync();
        }
    }
}
