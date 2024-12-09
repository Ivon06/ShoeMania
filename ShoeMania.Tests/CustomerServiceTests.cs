using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
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
    public class CustomerServiceTests
    {
        private ICustomerService customerService;
        private Mock<IRepository> repoMock;

        private List<User> users = new List<User>()
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

        private List<Customer> customers = new List<Customer>()
        {
            new Customer()
        {
            UserId = "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3",
            Id = "d1d73a5e-f042-436f-bcca-24b5537988e8"
        }
        };

        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            customerService = new CustomerService(repoMock.Object);
        }


        [Test]
        public async Task Create_ShouldAddCustomerWithCorrectUserId()
        {
            var userId = "074c3126-71b7-4e88-99c3-288c976de678";

            await customerService.Create(userId);

            repoMock.Verify(r => r.AddAsync(It.Is<Customer>(c => c.UserId == userId)), Times.Once);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        [TestCase("hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3")]
        public async Task GetCustomerIdByUserIdAsyncShouldReturnCorrectResult(string userId)
        {
            var mockUsers = users.BuildMock();
            var mockCustomers = customers.BuildMock();

            repoMock.Setup(r => r.GetAll<Customer>())
                .Returns(mockCustomers);

            var result = await customerService.GetCustomerIdByUserIdAsync(userId);
            var expectedResult = "d1d73a5e-f042-436f-bcca-24b5537988e8";

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]
        [TestCase("ccff2c3ce94a1")]
        [TestCase("null")]
        [TestCase("id")]
        public async Task GetCustomerIdByUserIdAsyncShouldReturnNull(string userId)
        {
            var mockCustomers = customers.BuildMock();

            repoMock.Setup(r => r.GetAll<Customer>())
                .Returns(mockCustomers);

            var result = await customerService.GetCustomerIdByUserIdAsync(userId);
            Assert.That(result, Is.Null);

        }
    }
}
