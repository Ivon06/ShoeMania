using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Sizes;
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
    public class SizeServiceTests
    {
        private static List<Size> sizes = new List<Size>()
            {
                new Size()
                {
                    Id = 1,
                    Number = 40
                },
                new Size()
                {
                    Id = 2,
                    Number = 41
                },
                new Size()
                {
                    Id = 3,
                    Number = 42
                },
                new Size()
                {
                    Id = 4,
                    Number = 43
                },
                new Size()
                {
                    Id = 5,
                    Number = 44
                }

            };

        private ISizeService sizeService;
        private Mock<IRepository> repoMock;

        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            sizeService = new SizeService(repoMock.Object);
        }

        [Test]
        public async Task AddSizesToShoeAsync_ShouldAddSizesToShoe()
        {
            // Arrange
            var sizesIds = new List<int> { 1, 2, 3 };
            var shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var addedSizes = new List<SizeShoe>();
           

            // Mock AddRangeAsync to capture added sizes
            repoMock.Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<SizeShoe>>()))
                .Callback<IEnumerable<SizeShoe>>(sizes => addedSizes.AddRange(sizes))
                .Returns(Task.CompletedTask);

            // Mock SaveChangesAsync
            repoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(3); // Simulate 3 rows affected

           
            await sizeService.AddSizesToShoeAsync(sizesIds, shoeId);

            // Assert
            Assert.That(addedSizes.Count, Is.EqualTo(sizesIds.Count));
            foreach (var sizeId in sizesIds)
            {
                Assert.That(addedSizes.Any(ss => ss.ShoeId == shoeId && ss.SizeId == sizeId), Is.True);
            }

            repoMock.Verify(r => r.AddRangeAsync(It.Is<IEnumerable<SizeShoe>>(ss =>
                ss.All(s => s.ShoeId == shoeId && sizesIds.Contains(s.SizeId))
            )), Times.Once);

            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }


        [Test]

        public async Task GetAllSizesAsyncShouldReturnCorrectResult()
        {
            var mock = sizes.AsQueryable();

            repoMock.Setup(r => r.GetAll<Size>())
                .Returns(mock.BuildMockDbSet().Object);

            var result = await sizeService.GetAllSizesAsync();

            var expectedResult = new List<SizeViewModel>()
            {
                new SizeViewModel()
                {
                    Id = 1,
                    Number = 40
                },
                new SizeViewModel()
                {
                    Id = 2,
                    Number = 41
                },
                new SizeViewModel()
                {
                    Id = 3,
                    Number = 42
                },
                new SizeViewModel()
                {
                    Id = 4,
                    Number = 43
                },
            };

            Assert.That(result[0].Id, Is.EqualTo(expectedResult[0].Id));
        }
    }
}
