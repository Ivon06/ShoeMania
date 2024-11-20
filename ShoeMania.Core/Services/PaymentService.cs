using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Payment;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository repo;

       

        public PaymentService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddOrderToPaymentAsync(string paymentId, string orderId)
        {
            var payment = await repo.GetAll<Payment>()
                .FirstOrDefaultAsync(p => p.Id == paymentId);

            if (payment == null)
            {
                return;
            }

            payment!.OrderId = orderId;

            await repo.SaveChangesAsync();

        }

        public async Task<string> CreatePaymentAsync(PaymentFormModel model, string customerId)
        {
            string[] el = model.ExpirationDate.Split('/');
            string expiryDate = $"01/{el[0]}/20{el[1]}";

            var payment = new Payment()
            {
                CardNumber = model.CardNumber,
                SecurityCode = model.SecurityCode,
                ExpityDate = DateTime.ParseExact(expiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                CardHolder = model.CardHolderName,
                CustomerId = customerId,

            };

            await repo.AddAsync(payment);
            await repo.SaveChangesAsync();

            return payment.Id;
        }

        
    }
}
