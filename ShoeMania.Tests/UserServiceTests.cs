using MockQueryable.Moq;
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
    public class UserServiceTests
    {
        private IUserService userService;
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
                PhoneNumber = "0889864831",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Kokiche 14",
                ProfilePictureUrl = "image"
            },
             new User()
            {
                Id = "5ae09e63-5bd2-470e-ae11-f96a7469c78c",
                FirstName = "Ivon",
                LastName = "Mircheva",
                Email = "ivonmircheva@abv.bg",
                NormalizedEmail = "IVONMIRCHEVA@ABV.BG",
                UserName = "ivon06",
                NormalizedUserName = "IVON06",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Stefan Stambolov 20",
                PhoneNumber = "0899464821",
                ProfilePictureUrl = "IMAGE"
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
            userService = new UserService(repoMock.Object);
        }

        [Test]
        public async Task ExistsByEmailAsyncShouldReturnTrue()
        {
            string email = "georgiivanov@gmail.com";

            var usersMock = users.AsQueryable();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock.BuildMockDbSet().Object);

            var result = await userService.ExistsByEmailAsync(email);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("Test@abv.bg")]
        [TestCase("test@abv.bg")]
        [TestCase("iavn@abv.bg")]
        public async Task ExistsByEmailAsyncShouldReturnFalse(string email)
        {
            var usersMock = users.AsQueryable();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock.BuildMockDbSet().Object);

            var result = await userService.ExistsByEmailAsync(email);

            Assert.That(result, Is.False);
        }


        [Test]
        public async Task ExistsByPhoneAsyncShouldReturnTrue()
        {
            string phone = "0889864831";

            var usersMock = users.AsQueryable();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock.BuildMockDbSet().Object);

            var result = await userService.ExistsByPhoneAsync(phone);

            Assert.That(result, Is.True);

        }

        [Test]
        [TestCase("0889875674")]
        [TestCase("0889653245")]
        [TestCase("0987645632")]
        public async Task ExistsByPhoneAsyncShouldReturnFalse(string phone)
        {
            var usersMock = users.AsQueryable();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock.BuildMockDbSet().Object);

            var result = await userService.ExistsByPhoneAsync(phone);


            Assert.That(result, Is.False);
        }

        [Test]
        public async Task IsCustomerAsyncShouldReturnTrue()
        {
            string userId = "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3";

            var customerMock = customers.AsQueryable();

            repoMock.Setup(r => r.GetAll<Customer>())
                .Returns(customerMock.BuildMockDbSet().Object);

            var result = await userService.IsCustomerAsync(userId);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("5ae09e63-5bd2-470e-ae11-f96a7469c78c")]
        [TestCase("someid")]
        [TestCase("047hfibvrcij3bq2ijbhuc-efwkjib")]
        public async Task IsCustomerShouldReturnFalse(string userId)
        {
            var customerMock = customers.AsQueryable();

            repoMock.Setup(r => r.GetAll<Customer>())
                .Returns(customerMock.BuildMockDbSet().Object);

            var result = await userService.IsCustomerAsync(userId);

            Assert.That(result, Is.False);
        }

        [Test]
        [TestCase("hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3")]
        [TestCase("5ae09e63-5bd2-470e-ae11-f96a7469c78c")]
        public async Task IsExistsByIdAsyncShouldReturnTrue(string userId)
        {
            var usersMock = users.AsQueryable();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock.BuildMockDbSet().Object);

            var result = await userService.IsExistsByIdAsync(userId);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("5ae09e63-5bd2-470e-ae11-f96a7469c79c")]
        [TestCase("someid")]
        [TestCase("047hfibvrcij3bq2ijbhuc-efwkjib")]
        public async Task IsExistsByIdAsyncShouldReturnFalse(string userId)
        {

            var usersMock = users.AsQueryable();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock.BuildMockDbSet().Object);

            var result = await userService.IsExistsByIdAsync(userId);

            Assert.That(result, Is.False);
        }

    }
}
