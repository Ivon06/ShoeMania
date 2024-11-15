using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Shoes
{
    public class PreDeleteShoeViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string ShoePictureUrl { get; set; } = null!;
    }
}
