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
    public class DeliveryOfficeServiceTests
    {

        private static List<DeliveryOffice> offices = new List<DeliveryOffice>()
        {
            new DeliveryOffice()
            {
                Id = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                Name = "ВАРНА - ЛЕВСКИ (КВ.)",
                Address = "ул. СТУДЕНТСКА 4 ДО ВХ. Д"
            }
        };

        private Mock<IRepository> repoMock;
        private IDeliveryOfficeService deliveryService;

        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            deliveryService = new DeliveryOfficeService(repoMock.Object);
        }

        [Test]
        public async Task GetDeliveryOfficeListAsyncShouldRetunCorrectResult()
        {
            var mock = offices.AsQueryable();

            repoMock.Setup(r => r.GetAll<DeliveryOffice>())
                .Returns(mock.BuildMockDbSet().Object);

            var result = await deliveryService.GetDeliveryOfficeListAsync();

            Assert.That(result, Is.Not.Null);
        }
    }
}
