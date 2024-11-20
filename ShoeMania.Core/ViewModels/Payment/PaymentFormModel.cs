using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.ViewModels.Payment
{
    public class PaymentFormModel
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string CardHolderName { get; set; } = null!;

        [Required]
        [StringLength(24, MinimumLength = 16)]
        public string CardNumber { get; set; } = null!;

        [Required]
        public string ExpirationDate { get; set; } = null!;

        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string SecurityCode { get; set; } = null!;


    }
}
