using ShoeMania.Core.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(PaymentFormModel model, string customerId);

        Task AddOrderToPaymentAsync(string paymentId, string orderId);
    }
}
