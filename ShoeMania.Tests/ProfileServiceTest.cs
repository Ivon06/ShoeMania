using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Profile;
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
    public class ProfileServiceTest
    {
        private static List<User> users = new List<User>()
        {
            new User()
            {
                Id = "cf736628-fe9b-4e17-9fe9-cff2c3ce94a1",
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
            },
            new User()
            {
                Id = "df733828-jse9b-ks17-99wk9-cxz2c3sk94a1",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivanivanov@gmail.com",
                NormalizedEmail = "IVANIVANOV@GMAIL.COM",
                UserName = "Ivcho",
                NormalizedUserName = "IVCHO",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Kokiche 19",
                ProfilePictureUrl = "image"
            }

        };

        private List<Customer> customers = new List<Customer>()
        {
            new Customer()
        {
            UserId = "cf736628-fe9b-4e17-9fe9-cff2c3ce94a1",
            Id = "d1d73a5e-f042-436f-bcca-24b5537988e8",
            User = users[0]
        }
        };

        private IProfileService profileService;
        private Mock<IImageService> imageServiceMock;
        private Mock<IRepository> repoMock;

        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            imageServiceMock = new Mock<IImageService>();
            profileService = new ProfileService(imageServiceMock.Object, repoMock.Object);

        }

        [Test]
        public async Task EditProfileAsyncShouldEditProfileCorrectly()
        {
            string userId = "cf736628-fe9b-4e17-9fe9-cff2c3ce94a1";
            EditProfileViewModel model = new EditProfileViewModel()
            {

                FirstName = "Georgi",
                LastName = "Ivanov",
                Email = "georgiivanov@gmail.com",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Kokiche 14",
                ProfilePictureUrl = "image"
            };

            var usersMock = users.BuildMock();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock);

            await profileService.EditProfileAsync(userId, model);

            repoMock.Setup(r => r.GetByIdAsync<User>(userId))
            .ReturnsAsync(usersMock.First(r => r.Id == userId));

            var profile = await repoMock.Object.GetByIdAsync<User>(userId);

            Assert.Multiple(() =>
            {
                Assert.That(profile.FirstName, Is.EqualTo(model.FirstName));
                Assert.That(profile.LastName, Is.EqualTo(model.LastName));
                Assert.That(profile.Email, Is.EqualTo(model.Email));
                Assert.That(profile.City, Is.EqualTo(model.City));
            });
        }

        [Test]
        [TestCase("null")]
        [TestCase("id")]
        [TestCase("112")]
        public async Task EditProfileAsyncShouldDoNothing(string userId)
        {
            EditProfileViewModel model = new EditProfileViewModel()
            {

                FirstName = "Georgi",
                LastName = "Ivanov",
                Email = "georgiivanov@gmail.com",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Kokiche 14",
                ProfilePictureUrl = "image"
            };

            var usersMock = users.BuildMock();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock);

            await profileService.EditProfileAsync(userId, model);

            repoMock.Setup(r => r.GetByIdAsync<User>(userId))
            .ReturnsAsync(usersMock.FirstOrDefault(r => r.Id == userId));

            var profile = await repoMock.Object.GetByIdAsync<User>(userId);

            Assert.That(profile, Is.Null);
        }

        [Test]
        public async Task GetProfileAsyncShouldReturnCorrectResult()
        {
            string userId = "cf736628-fe9b-4e17-9fe9-cff2c3ce94a1";
            var model = new ProfileViewModel()
            {
                Id = userId,
                Email = "georgiivanov@gmail.com",
                Name = "Georgi Ivanov",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Kokiche 14",
                ProfilePictureUrl = "image"
            };

            var customersMock = customers.AsQueryable();

            repoMock.Setup(r => r.GetAll<Customer>())
                .Returns(customersMock.BuildMockDbSet().Object);


            var result = await profileService.GetProfileAsync(userId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(model.Name));
                Assert.That(result.City, Is.EqualTo(model.City));
                Assert.That(result.Country, Is.EqualTo(model.Country));
                Assert.That(result.Address, Is.EqualTo(model.Address));
            });

        }

        [Test]
        [TestCase("null")]
        [TestCase("id")]
        [TestCase("112")]
        public async Task GetProfileAsyncShouldReturnNull(string userId)
        {
            var customersMock = customers.BuildMock();

            repoMock.Setup(r => r.GetAll<Customer>())
                .Returns(customersMock);

            var result = await profileService.GetProfileAsync(userId);

            int A = 1;

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetProfileForEditAsyncShouldReturnCorrectResult()
        {
            string userId = "cf736628-fe9b-4e17-9fe9-cff2c3ce94a1";
            var model = new EditProfileViewModel()
            {
                FirstName = "Georgi",
                LastName = "Ivanov",
                Email = "georgiivanov@gmail.com",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Al. Batenberg 15",
                Phone = "0889864831",
                ProfilePictureUrl = "https://res.cloudinary.com/ddtdhqc02/image/upload/v1733932932/ShoeManiaProject/jbebohqxeunabj8o4wgv.webp"
            };

            var usersMock = users.BuildMock();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock);

            var result = await profileService.GetProfileForEditAsync(userId);

            Assert.Multiple(() =>
            {
                Assert.That(result.FirstName, Is.EqualTo(model.FirstName));
                Assert.That(result.LastName, Is.EqualTo(model.LastName));
                Assert.That(result.Email, Is.EqualTo(model.Email));
                Assert.That(result.City, Is.EqualTo(model.City));
            });
        }

        [Test]
        [TestCase("null")]
        [TestCase("id")]
        [TestCase("112")]
        public async Task GetProfileForEditAsyncShouldReturnNull(string userId)
        {
            var usersMock = users.BuildMock();

            repoMock.Setup(r => r.GetAll<User>())
                .Returns(usersMock);

            var result = await profileService.GetProfileForEditAsync(userId);

            Assert.That(result, Is.Null);
        }

    }
}
