using ShoeMania.Core.ViewModels.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Shoes
{
    public class OrderShoeViewModel
    {
        public string Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Size { get; set; }

        public string ShoeImageUrl { get; set; } = null!;

        public List<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();

        public bool IsEnabled { get; set; } = true;

    }
}
