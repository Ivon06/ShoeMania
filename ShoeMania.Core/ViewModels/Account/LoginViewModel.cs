using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 3)]
        public string Password { get; set; } = null!;
    }
}
