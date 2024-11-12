using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Profile
{
    public class EditProfileViewModel
    {
        
            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string FirstName { get; set; } = null!;

            [Required]
            [StringLength(30, MinimumLength = 3)]
            public string LastName { get; set; } = null!;

            [Required]
            [StringLength(30, MinimumLength = 3)]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(200, MinimumLength = 3)]
            public string City { get; set; } = null!;

            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string Country { get; set; } = null!;

            [Required]
            [StringLength(200, MinimumLength = 10)]
            public string Address { get; set; } = null!;

            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string Phone { get; set; } = null!;

            public string? ProfilePictureUrl { get; set; }

            public IFormFile? ProfilePicture { get; set; }
        }
    }

