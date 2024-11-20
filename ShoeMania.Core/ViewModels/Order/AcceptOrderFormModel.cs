using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Order
{
    public class AcceptOrderFormModel
    {
        public AcceptOrderFormModel()
        {
            this.Shoes = new HashSet<OrderedShoeInfo>();
        }
        public string Id { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string OrderTime { get; set; } = null!;

        public DateTime DeliveryTime { get; set; }

        public string DeliveryAddress { get; set; } = null!;

        public decimal Price { get; set; }

        public string Status { get; set; } = null!;

        public ICollection<OrderedShoeInfo> Shoes { get; set; }
    }
}
