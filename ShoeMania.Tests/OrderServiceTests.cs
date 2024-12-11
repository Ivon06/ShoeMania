using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Order;
using ShoeMania.Core.ViewModels.Shoes;
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
    public class OrderServiceTests
    {
        private Mock<IRepository> repoMock;
        private IOrderService orderService;
        
      
        private static List<DeliveryOffice> offices = new List<DeliveryOffice>()
        { 
            new DeliveryOffice()
            {
                Id = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                Name = "ВАРНА - ЛЕВСКИ (КВ.)",
                Address = "ул. СТУДЕНТСКА 4 ДО ВХ. Д"
            }};

        private static List<User> users = new List<User>()
        {
            new User()
            {
                Id = "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3",
                FirstName = "Georgi",
                LastName = "Ivanov",
                Email = "georgiivanov@gmail.com",
                NormalizedEmail = "GEORGIIVANOV@GMAIL.COM",
                UserName = "Gosho",
                NormalizedUserName = "GOSHO",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Kokiche 14",
                ProfilePictureUrl = "image"
            }
        };

        private static List<Customer> customers = new List<Customer>()
        {
            new Customer()
        {
            UserId = "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3",
            Id = "d1d73a5e-f042-436f-bcca-24b5537988e8",
            User = users[0]
        }
        };

       

        private static List<Shoe> shoes = new List<Shoe>()
        {
            new Shoe()
            {
                Id = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4",
                Name = "Test Shoe",
                CategoryId = "08851195-d523-418f-b272-37c3d91544df",
                Description = "test descritpion for shoe",
                Price = 13.3m,
                ShoeUrlImage = "image",
                IsActive = true,
               
            }
        };

        private static List<OrderShoe> ordersShoe = new List<OrderShoe>()
        {
            new OrderShoe()
            {
                Shoe = shoes[0]
            }
        };

        private List<Order> orders = new List<Order>()
        {
            new Order()
            {
                Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                Status = "Waiting",
                CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
                OrderTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(1),
                DeliveryOfficeId = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                Price = 120.0m,
                DeliveryOffice = offices[0],
                Customer = customers[0],

            },
            new Order()
            { 
                Id ="sa345fla7-2e6f-4as43-a728-d1d64a57e865",
                Status = "Waiting",
                CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
                OrderTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(1),
                DeliveryOfficeId = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                Price = 120.0m,
                DeliveryOffice = offices[0],
                Customer = customers[0],

            }
        };




        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            orderService = new OrderService(repoMock.Object);
            
        }

        [Test]
        public async Task AddDeliveryTimeForOrderAsyncShouldAddDeliveryTime()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                CustomerName = "Test Testov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Waiting",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
                .Returns(ordersMock);

            await orderService.AddDeliveryTimeForOrderAsync(model);

            repoMock.Setup(r => r.GetByIdAsync<Order>(model.Id))
                 .ReturnsAsync(orders[0]);

            var result = await repoMock.Object.GetByIdAsync<Order>(model.Id);

           Assert.That(result, Is.Not.Null);
           Assert.That(result.DeliveryTime, Is.EqualTo(model.DeliveryTime));

        }

        [Test]
        public async Task AddDeliveryTimeForOrderAsyncShouldDoNothig()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "73-4143-a728-d1d64a57e865",
                CustomerName = "Test Testov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Waiting",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
                .Returns(ordersMock);

            await orderService.AddDeliveryTimeForOrderAsync(model);

            var result = await orderService.GetOrderByIdAsync(model.Id);

            Assert.That(result, Is.Null);

        }

        [Test]
        [TestCase("Accepted")]
        [TestCase("Delivered")]
        public async Task ChangeStatusOrderAsyncShouldChangeOrderStatus(string status)
        {
            string orderId = "7345f9f7-2e6f-4143-a728-d1d64a57e865";

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
                .Returns(ordersMock);

            await orderService.ChangeStatusOrderAsync(orderId, status);


            repoMock.Setup(r => r.GetByIdAsync<Order>(orderId))
                 .ReturnsAsync(ordersMock.FirstOrDefault(o=>o.Id == orderId));

            var order = await repoMock.Object.GetByIdAsync<Order>(orderId);

            Assert.That(order, Is.Not.Null);
            Assert.That(order.Status, Is.EqualTo(status));
        }

        [Test]
        [TestCase("Accepted")]
        [TestCase("Delivered")]
        public async Task ChangeStatusOrderAsyncShouldDoNothing(string status)
        {
            string orderId = "7345f9f7d64a57e865";

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
                .Returns(ordersMock);

            await orderService.ChangeStatusOrderAsync(orderId, status);

            repoMock.Setup(r => r.GetByIdAsync<Order>(orderId))
                 .ReturnsAsync(ordersMock.FirstOrDefault(o => o.Id == orderId));

            var order = await repoMock.Object.GetByIdAsync<Order>(orderId);

            Assert.That(order, Is.Null);
        }


        [Test]
        public async Task CreateOrderAsyncShouldCreateOrder()
        {
            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
        .Callback<Order>(order => ordersMock.ToList().Add(order))
        .Returns(Task.CompletedTask);

            // Mock SaveChangesAsync
            repoMock.Setup(r => r.SaveChangesAsync());

            // Mock GetByIdAsync to retrieve the created order
            repoMock.Setup(r => r.GetByIdAsync<Order>(It.IsAny<string>()))
                .ReturnsAsync((string id) => ordersMock.FirstOrDefault(o => o.Id == id));

            var orderService = new OrderService(repoMock.Object);

            var payment = new Payment
            {
                Id = "ca891e41-3e5a-4abd-ba6b-faae2b318ea3",
                CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
                CardNumber = "0123456789101112",
                CardHolder = "Test Testov",
                ExpityDate = DateTime.Now.AddYears(2),
                SecurityCode = "8972"
            };

            await repoMock.Object.AddAsync(payment);
            await repoMock.Object.SaveChangesAsync();

            var model = new OrderFormModel
            {
                DeliveryOfficeId = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                City = "Kazanlak",
                PaymentId = payment.Id,
                Shoes = new List<OrderShoeViewModel>
        {
            new OrderShoeViewModel
            {
                Id = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4",
                Name = "Test Shoe",
                Description = "test description for shoe",
                Price = 13.3m,
                ShoeImageUrl = "null",
                Size = 40
            }
        }
            };

            string customerId = "d1d73a5e-f042-436f-bcca-24b5537988e8";

            // Act
            var result = await orderService.CreateOrderAsync(model, customerId);

            var order = await orderService.GetOrderByIdAsync(result);

            // Assert
            Assert.That(order, Is.Not.Null);
            
        }

        [Test]
        public async Task EditDeliveryTimeForOrderAsyncShouldEditDeliveryTime()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                CustomerName = "Test Testov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Waiting",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
                .Returns(ordersMock);


            await orderService.EditDeliveryTimeForOrderAsync(model);

            repoMock.Setup(r => r.GetByIdAsync<Order>(model.Id))
                  .ReturnsAsync(ordersMock.FirstOrDefault(o => o.Id == model.Id));

            var order = await repoMock.Object.GetByIdAsync<Order>(model.Id);

            Assert.That(order, Is.Not.Null);
            Assert.That(order.DeliveryTime, Is.EqualTo(model.DeliveryTime));

        }

        [Test]
        public async Task EditDeliveryTimeForOrderAsyncShouldDoNothing()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f9f7-2ed64a57e865",
                CustomerName = "Test Testov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Waiting",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
                .Returns(ordersMock);


            await orderService.EditDeliveryTimeForOrderAsync(model);

            repoMock.Setup(r => r.GetByIdAsync<Order>(model.Id))
                  .ReturnsAsync(ordersMock.FirstOrDefault(o => o.Id == model.Id));

            var order = await repoMock.Object.GetByIdAsync<Order>(model.Id);

            Assert.That(order, Is.Null);
        }


        [Test]
        public async Task GetAllOrdersAsyncShouldReturnCorrectResult()
        {
            var ordersMock = orders.AsQueryable();

            repoMock.Setup(r => r.GetAll<Order>())
               .Returns(ordersMock.BuildMockDbSet().Object);

            var result = await orderService.GetAllOrdersAsync();

            var expectedResult = new List<OrderViewModel>()
            {
                new OrderViewModel()
                {
                    Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                    DeliveryAddress = "Test address",
                    Status = "Waiting",
                    CustomerPhoneNumber = "0889864831",
                    Price = 120.0m,
                    OrderTime = DateTime.Now.ToString("dddd, dd MMMM yyyy"),
                    DeliveryTime = DateTime.Now.AddHours(1).ToString("dddd, dd MMMM yyyy"),

                }
            };

            CollectionAssert.IsNotEmpty(result);
            CollectionAssert.IsNotEmpty(expectedResult);
            Assert.That(result[0].Id, Is.EqualTo(expectedResult[0].Id));
        }

        [Test]
        public async Task GetCustomerOrdersAsyncShouldReturnCorrectResult()
        {
            string customerId = "d1d73a5e-f042-436f-bcca-24b5537988e8";
            var ordersMock = orders.AsQueryable();

            repoMock.Setup(r => r.GetAll<Order>())
               .Returns(ordersMock.BuildMockDbSet().Object);

            var result = await orderService.GetCustomerOrdersAsync(customerId);

            var expectedResult = new List<OrderViewModel>()
            {
                new OrderViewModel()
                {
                    Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                    DeliveryAddress = "Test address",
                    Status = "Waiting",
                    CustomerPhoneNumber = "0889864831",
                    Price = 120.0m,
                    OrderTime = DateTime.Now.ToString("dddd, dd MMMM yyyy"),
                    DeliveryTime = DateTime.Now.AddHours(1).ToString("dddd, dd MMMM yyyy"),

                }
            };

            Assert.That(result,Is.Not.Empty);
            Assert.That(expectedResult, Is.Not.Empty);
           
            //Assert.That(result[0].Id, Is.EqualTo(expectedResult[0].Id));
        }


        [Test]
        public async Task GetCustomerOrdersAsyncShouldReturnEmptyCollection()
        {
            string customerId = "d1d73a5e-f04237988e8";

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
              .Returns(ordersMock);

            var result = await orderService.GetCustomerOrdersAsync(customerId);

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public async Task GetOrderByIdAsyncShouldReturnCorrectResult()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                CustomerName = "Georgi Ivanov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Delivered",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
              .Returns(ordersMock);

            var result = await orderService.GetOrderByIdAsync(model.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(model.Id));
                Assert.That(result.CustomerName, Is.EqualTo(model.CustomerName));
                Assert.That(result.Status, Is.EqualTo(model.Status));
            });
        }

        [Test]
        public async Task GetOrderByIdAsyncShouldReturnNull()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f943-a728-d1d64a57e865",
                CustomerName = "Georgi Ivanov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Waiting",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
              .Returns(ordersMock);


            var result = await orderService.GetOrderByIdAsync(model.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetOrderForEditShouldReturnCorrectResult()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                CustomerName = "Georgi Ivanov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Delivered",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.AsQueryable();

            repoMock.Setup(r => r.GetAll<Order>())
               .Returns(ordersMock.BuildMockDbSet().Object);

            var result = await orderService.GetOrderForEditByIdAsync(model.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(model.Id));
                Assert.That(result.CustomerName, Is.EqualTo(model.CustomerName));
                Assert.That(result.Status, Is.EqualTo(model.Status));
            });

        }

        [Test]
        public async Task GetOrderForEditShouldReturnNull()
        {
            AcceptOrderFormModel model = new AcceptOrderFormModel()
            {
                Id = "7345f9f7-728-d1d64a57e865",
                CustomerName = "Georgi Ivanov",
                OrderTime = "",
                DeliveryAddress = "Test address",
                Price = 120.0m,
                Status = "Waiting",
                DeliveryTime = DateTime.Now.AddMinutes(30),
            };

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
              .Returns(ordersMock);


            var result = await orderService.GetOrderForEditByIdAsync(model.Id);

            Assert.That(result, Is.Null);

        }

        [Test]
        public async Task IsOrderExistsByIsAsyncShouldReturnTrue()
        {

            string orderId = "7345f9f7-2e6f-4143-a728-d1d64a57e865";

            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
              .Returns(ordersMock);

            var result = await orderService.IsOrderExistsAsync(orderId);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase("null")]
        [TestCase("id")]
        public async Task IsOrderExistsByIsAsyncShouldReturnFalse(string orderId)
        {
            var ordersMock = orders.BuildMock();

            repoMock.Setup(r => r.GetAll<Order>())
              .Returns(ordersMock);


            var result = await orderService.IsOrderExistsAsync(orderId);

            Assert.That(result, Is.False);
        }
    }
}
