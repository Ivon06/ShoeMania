using ShoeMania.Core.ViewModels.Category;
using ShoeMania.Core.ViewModels.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface ICategoryService
    {
        Task<List<string>> GetAllCategoryNamesAsync();

        Task<List<CategoryViewModel>> GetAllCategoriesAsync();
    }
}
