using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.ViewModels.Category;
using ShoeMania.Core.ViewModels.Sizes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoeMania.Common.Constants.ShoeConstants;

namespace ShoeMania.Core.ViewModels.Shoes
{
    public class ShoeFormModel
    {
        public ShoeFormModel()
        {
            this.Sizes = new HashSet<SizeViewModel>();
            this.Categories = new HashSet<CategoryViewModel>();
            this.SizeIds = new List<int>();
        }

        [StringLength(ShoeNameMaxLength, MinimumLength = ShoeNameMinLength)]
        public string Name { get; set; } = null!;

        [StringLength(ShoeDescriptionMaxLength, MinimumLength = ShoeDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Precision(18, 2)]
        public string Price { get; set; }

        public IFormFile? ShoeUrlImage { get; set; }

        public List<int> SizeIds { get; set; }

        public ICollection<SizeViewModel> Sizes { get; set; }

        public string? CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
