using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Category;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repo;

        public CategoryService(IRepository repo)
        {
            this.repo = repo;
        }
        public async Task<List<string>> GetAllCategoryNamesAsync()
        {
            var categories = await repo.GetAll<Category>()
                .Select(c => c.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await repo.GetAll<Category>()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return categories;
        }
    }
}
