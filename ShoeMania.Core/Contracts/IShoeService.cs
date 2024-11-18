using ShoeMania.Core.ViewModels.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IShoeService
    {
        Task<AllShoesFilteredAndPaged> GetAllShoesFilteredAndPagedAsync(ShoesQueryModel model);

        Task<string> AddAsync(ShoeFormModel model);

        Task<bool> IsExistsAsync(string id);

        Task<ShoeFormModel> GetShoeForEditAsync(string shoeId);

        Task EditShoeAsync(ShoeFormModel shoe, string shoeId);

        Task<PreDeleteShoeViewModel> GetShoeForDeleteAsync(string shoeId);

        Task DeleteShoeAsync(string shoeId);

        Task<DetailsShoeViewModel> GetDetailsForShoeAsync(string shoeId);

    }
}
