using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoeMania.Common.Constants.CategoryConstants;

namespace ShoeMania.Data.Models
{
	public class Category
	{
		public Category()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		[Key]
		public string Id { get; set; }

		[MaxLength(CategoryNameMaxLength)]
		public string Name { get; set; } = null!;
	}
}
