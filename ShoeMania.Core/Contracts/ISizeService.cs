using ShoeMania.Core.ViewModels.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface ISizeService
    {
        Task<List<SizeViewModel>> GetAllSizesAsync();

        Task AddSizesToShoeAsync(List<int> sizesIds, string shoeId);
    }
}
