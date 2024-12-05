using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static ShoeMania.Common.Constants.DeliveryOfficeConstants;

namespace ShoeMania.Data.Models
{
	public class DeliveryOffice
	{
        public DeliveryOffice()
        {
            Id = Guid.NewGuid().ToString();
            Orders = new HashSet<Order>();
        }

        [Key]
		public string Id { get; set; } = null!;

        [Required]
        [MaxLength(OfficeNameMaxLength)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(OfficeAddressMaxLength)]
        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;

        public ICollection<Order> Orders { get; set; }
	}
}
