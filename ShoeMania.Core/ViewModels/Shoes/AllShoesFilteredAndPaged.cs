using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Shoes
{
    public class AllShoesFilteredAndPaged
    {
        public AllShoesFilteredAndPaged()
        {
            this.Shoes = new HashSet<ShoeViewModel>();
        }
        public int TotalShoes { get; set; }

        public IEnumerable<ShoeViewModel> Shoes { get; set; }
    }
}
