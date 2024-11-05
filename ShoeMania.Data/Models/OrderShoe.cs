using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Models
{
	public class OrderShoe
	{
		[ForeignKey(nameof(Order))]
		public string OrderId { get; set; } = null!;

		public Order Order { get; set; } = null!;

		[ForeignKey(nameof(Shoe))]
		public string ShoeId { get; set; } = null!;

		public Shoe Shoe { get; set; } = null!;

		public int ShoeSize { get; set; }
	}
}
