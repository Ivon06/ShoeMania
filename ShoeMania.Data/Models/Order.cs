using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeMania.Data.Models.Enums;

namespace ShoeMania.Data.Models
{
	public class Order
	{
		public Order()
		{
			this.Id = Guid.NewGuid().ToString();
			this.OrderShoe = new HashSet<OrderShoe>();
			this.OrderTime = DateTime.Now;

		}

		[Key]
		public string Id { get; set; }

		[Required]
		[EnumDataType(typeof(OrderStatusEnum))]
		public string Status { get; set; } = null!;

		[ForeignKey(nameof(Customer))]
		public string CustomerId { get; set; } = null!;

		public Customer Customer { get; set; } = null!;

		public DateTime OrderTime { get; set; }

		public DateTime? DeliveryTime { get; set; }

		[ForeignKey(nameof(DeliveryOffice))]
		public string DeliveryOfficeId { get; set; } = null!;

		public DeliveryOffice DeliveryOffice { get; set; } = null!;
		public decimal Price { get; set; }

		[ForeignKey(nameof(Payment))]
		public string? PaymentId { get; set; }

		public Payment? Payment { get; set; }

		public ICollection<OrderShoe> OrderShoe { get; set; }



	}
}
