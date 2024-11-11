using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Account
{
    public class RegisterViewModel
    {

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string LastName { get; set; } = null!;


        [Required]
        [EmailAddress]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 3)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 4)]
        public string PasswordRepeat { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Address { get; set; } = null!;

        public IFormFile? ProfilePicture { get; set; }
    }
}
