using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Models
{
	public class Size
	{
		public Size()
		{
			this.SizeShoe = new HashSet<SizeShoe>();

		}
		[Key]
		public int Id { get; set; }

		public int Number { get; set; }

		public ICollection<SizeShoe> SizeShoe { get; set; }
	}
}
