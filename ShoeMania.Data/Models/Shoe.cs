using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static ShoeMania.Common.Constants.ShoeConstants;

namespace ShoeMania.Data.Models
{
	public class Shoe
	{

		public Shoe()
		{
			this.Id = Guid.NewGuid().ToString();
			this.IsActive = true;
			this.OrderShoe = new HashSet<OrderShoe>();
		}


		[Key]
		public string Id { get; set; }

		[MaxLength(ShoeNameMaxLength)]
		public string Name { get; set; } = null!;

		[ForeignKey(nameof(Category))]
		public string CategoryId { get; set; } = null!;

		public Category Category { get; set; }

		[MaxLength(ShoeDescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Precision(18, 2)]
		public decimal Price { get; set; }

		public string? ShoeUrlImage { get; set; }

		public bool IsActive { get; set; }

		public ICollection<OrderShoe> OrderShoe { get; set; }
		public ICollection<SizeShoe> SizeShoe { get; set; }
	}
}
