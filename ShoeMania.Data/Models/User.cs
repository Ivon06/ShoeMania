using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoeMania.Common.Constants.UserConstants;

namespace ShoeMania.Data.Models
{
	public class User : IdentityUser
	{
		public User()
		{
			this.IsActive = true;
		}

		[MaxLength(FirstnameMaxLength)]
		public string FirstName { get; set; } = null!;


		[MaxLength(LastNameMaxLength)]
		public string LastName { get; set; } = null!;

		[MaxLength(CityMaxLength)]
		public string City { get; set; } = null!;

		[MaxLength(CountryMaxLength)]
		public string Country { get; set; } = null!;

		[MaxLength(AddressMaxLength)]
		public string Address { get; set; } = null!;

		public string? ProfilePictureUrl { get; set; }

		public bool IsActive { get; set; }


	}
}
