using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Payment;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Order order = new Order()
        {
            Id = "de5c3a0f-a366-43e5-8edf-3c41732e2680",
            Status = "Waiting",
            CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
            OrderTime = DateTime.Now,
            DeliveryTime = DateTime.Now.AddHours(1),
            DeliveryOffice = new DeliveryOffice()
            {
                Name = "Test address",
                Address = "Address"
            },
            Price = 100.0m

        };

        private Payment payment = new Payment()
        {
            Id = "ca891e41-3e5a-4abd-ba6b-faae2b318ea3",
            CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
            CardNumber = "0123456789101112",
            CardHolder = "Test Testov",
            ExpityDate = DateTime.Now.AddYears(2),
            SecurityCode = "8972"
        };
        private IPaymentService paymentService;
        private Mock<IRepository> repoMock;

        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            paymentService = new PaymentService(repoMock.Object);
        }

        [Test]
        public async Task AddOrderToPaymentAsyncShouldReturnCorrectResult()
        {
           

            await repoMock.Object.AddAsync(payment);
            await repoMock.Object.AddAsync(order);



            await repoMock.Object.SaveChangesAsync();

            var paymentMock = new List<Payment>()
            { payment}.BuildMock();

            repoMock.Setup(r => r.GetAll<Payment>())
                .Returns(paymentMock);

            await paymentService.AddOrderToPaymentAsync(payment.Id, order.Id);

            repoMock.Setup(r => r.GetByIdAsync<Payment>(payment.Id))
                .ReturnsAsync(payment);

            var result = await repoMock.Object.GetByIdAsync<Payment>(payment.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.OrderId, Is.EqualTo(order.Id));
        }

        [Test]
        [TestCase("someId")]
        [TestCase("ca891e41-3e5a-4abd-ba6b-faae2b318ea3")]
        public async Task AddOrderToPaymentAsyncShouldDoNothing(string paymentId)
        {
            string orderId = Guid.NewGuid().ToString();

            var paymentMock = new List<Payment>()
            { payment}.BuildMock();

            repoMock.Setup(r => r.GetAll<Payment>())
            .Returns(paymentMock);

            await paymentService.AddOrderToPaymentAsync(payment.Id, order.Id);


            var result = await repoMock.Object.GetByIdAsync<Payment>(payment.Id);

            Assert.That(result, Is.Null);

        }

        [Test]
        public async Task CreatePaymentAsyncShouldCreatePaymentCorrectly()
        {
            PaymentFormModel model = new PaymentFormModel()
            {
                CardHolderName = "Test testov",
                CardNumber = "01234567891011",
                ExpirationDate = "02/25",
                SecurityCode = "9876"
            };

            string customerId = "d1d73a5e-f042-436f-bcca-24b5537988e8";

            var result = await paymentService.CreatePaymentAsync(model, customerId);

            Assert.That(result, Is.Not.Null);
            
        }
    }
}
