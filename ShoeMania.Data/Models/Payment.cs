using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoeMania.Common.Constants.PaymentConstants;

namespace ShoeMania.Data.Models
{
	public class Payment
	{
		public Payment()
		{
			this.Id = Guid.NewGuid().ToString();
		}
		[Key]
		public string Id { get; set; }

		[ForeignKey(nameof(Customer))]
		public string CustomerId { get; set; } = null!;

		public Customer Customer { get; set; } = null!;

		[ForeignKey(nameof(Order))]
		public string? OrderId { get; set; }

		public Order? Order { get; set; }

		[MaxLength(CardNumberMaxLength)]
		public string CardNumber { get; set; } = null!;

		[MaxLength(CardHolderLength)]
		public string CardHolder { get; set; } = null!;

		public DateTime ExpityDate { get; set; }

		[MaxLength(SecurityCodeLength)]
		public string SecurityCode { get; set; } = null!;
	}
}
