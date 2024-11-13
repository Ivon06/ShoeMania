using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Shoes
{
    public class ShoesQueryModel
    {
        public ShoesQueryModel()
        {
            this.Shoes = new List<ShoeViewModel>();
            this.Categories = new List<string>();
            this.CurrentPage = 1;
            this.ShoesPerPage = 6;
        }

        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Shoes Per Page")]
        public int ShoesPerPage { get; set; }

        public int TotalShoes { get; set; }

        public List<string> Categories { get; set; }

        public List<ShoeViewModel> Shoes { get; set; }
    }
}
