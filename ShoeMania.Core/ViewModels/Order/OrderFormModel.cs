using ShoeMania.Core.ViewModels.Shoes;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Order
{
    public class OrderFormModel
    {
        public OrderFormModel()
        {
            this.Shoes = new List<OrderShoeViewModel>();
            this.Offices = new List<DeliveryOffice>();
        }
        //[Required]
        //[StringLength(100, MinimumLength = 5)]
        //public string Address { get; set; } = null!;

        [Required]
        public string DeliveryOfficeId { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string City { get; set; } = null!;

        public ICollection<OrderShoeViewModel> Shoes { get; set; }

        public string PaymentId { get; set; }

        public ICollection<DeliveryOffice> Offices { get; set; }
    }
}
