using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Models
{
	public class Customer
	{
		public Customer()
		{
			this.Id = Guid.NewGuid().ToString();
			this.IsActive = true;
			this.Orders = new HashSet<Order>();
			this.Payments = new HashSet<Payment>();
		}


		[Key]
		public string Id { get; set; }


		[Required]
		[ForeignKey(nameof(User))]
		public string UserId { get; set; } = null!;
		public User User { get; set; } = null!;
		public bool IsActive { get; set; }

		public ICollection<Order> Orders { get; set; }

		public ICollection<Payment> Payments { get; set; }
	}
}
