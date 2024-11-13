using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoeMania.Common.Constants.UserConstants;

namespace ShoeMania.Core.ViewModels.Profile
{
    public class EditProfileViewModel
    {
        
            [Required]
            [StringLength(FirstnameMaxLength, MinimumLength = FirstNameMinLength)]
            public string FirstName { get; set; } = null!;

            [Required]
            [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
            public string LastName { get; set; } = null!;

            [Required]
            [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
            public string City { get; set; } = null!;

            [Required]
            [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
            public string Country { get; set; } = null!;

            [Required]
            [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
            public string Address { get; set; } = null!;

            [Required]
            [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
            public string Phone { get; set; } = null!;

            public string? ProfilePictureUrl { get; set; }

            public IFormFile? ProfilePicture { get; set; }
        }
    }

