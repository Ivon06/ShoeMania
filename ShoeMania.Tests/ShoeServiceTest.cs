using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Category;
using ShoeMania.Core.ViewModels.Shoes;
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
    public class ShoeServiceTest
    {
        private List<Shoe> shoes = new List<Shoe>()
        {
            new Shoe()
            {
                Id = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4",
                Name = "Test Shoe",
                CategoryId = "08851195-d523-418f-b272-37c3d91544df",
                Description = "test descritpion for shoe",
                Price = 13.3m,
                ShoeUrlImage = "image",
                IsActive = true
            }
        };

        private List<Size> sizes = new List<Size>()
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

        private List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = "08851195-d523-418f-b272-37c3d91544df",
                    Name = "Sports"
                },
                new Category()
                {
                    Id = "22ef8689-c7fa-4315-8505-377ece4c16ff",
                    Name = "Men"
                },
                new Category()
                {
                    Id = "2f22398c-dc3c-4d81-9a56-19a46252210c",
                    Name = "Women"
                },
                new Category()
                {
                    Id = "4874581a-3b02-41e0-bdca-07cf40545f88",
                    Name = "Kids"
                },

            };

        private List<SizeShoe> sizesShoe = new List<SizeShoe>()
            {
                new SizeShoe()
                {
                    SizeId = 1,
                    ShoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4"
                }
        };

        private Mock<IRepository> repoMock;
        private IShoeService shoeService;
        private Mock<IImageService> imageServiceMock;
        private IMock<IHttpContextAccessor> accessor;

        [SetUp]
        public async Task SetupBase()
        {
            repoMock = new Mock<IRepository>();
            imageServiceMock = new Mock<IImageService>();
            accessor = new Mock<IHttpContextAccessor>();
            shoeService = new ShoeService(repoMock.Object, imageServiceMock.Object, accessor.Object);
        }

        [Test]
        [TestCase("Test Shoe")]
        [TestCase("Test")]
        [TestCase("Shoe")]

        public async Task GetAllShoesFilteredAndPagedAsync(string searchString)
        {
            ShoesQueryModel model = new ShoesQueryModel()
            {
                Category = "Sports",
                SearchString = searchString,
            };

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            await repoMock.Object.AddRangeAsync<Size>(sizes);
            await repoMock.Object.AddRangeAsync<SizeShoe>(sizesShoe);
            await repoMock.Object.AddRangeAsync<Category>(categories);

            var result = await shoeService.GetAllShoesFilteredAndPagedAsync(model);

            var expectedResult = new AllShoesFilteredAndPaged()
            {
                Shoes = new List<ShoeViewModel>()
                {
                    new ShoeViewModel()
                    {
                        Id = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4",
                        Name = "Test Shoe",
                        Price = 13.3m,
                        ShoePictureUrl = "image",
                        Description = "test descritpion for shoe",
                    }
                },
                TotalShoes = 1
            };

            Assert.That(result.TotalShoes, Is.EqualTo(expectedResult.TotalShoes));
            Assert.That(result.Shoes.ToList()[0].Name, Is.EqualTo(result.Shoes.ToList()[0].Name));
            Assert.That(result.Shoes.ToList()[0].Id, Is.EqualTo(result.Shoes.ToList()[0].Id));
        }

        [Test]
        public async Task AddAsyncShouldAddShoeCorrectly()
        {
            ShoeFormModel model = new ShoeFormModel()
            {
                Name = "Test add shoe",
                Description = "description for test shoe",
                Price = "40",
                CategoryId = "08851195-d523-418f-b272-37c3d91544df",

            };

            var result = await shoeService.AddAsync(model);

            var expectedResult = await repoMock.Object.GetByIdAsync<Shoe>( result);

            Assert.That(expectedResult, Is.Not.Null);
            Assert.That(expectedResult.Name, Is.EqualTo(model.Name));
            Assert.That(expectedResult.Description, Is.EqualTo(model.Description));
        }

        [Test]
        public async Task GetDetailsForShoeAsyncShouldReturnCorrectResult()
        {
            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            var result = await shoeService.GetDetailsForShoeAsync(shoeId);

            var expectedResult = new DetailsShoeViewModel()
            {
                Id = shoeId,
                Name = "Test Shoe",
                Description = "test descritpion for shoe",
                Price = 13.3m,
                Category = "Sports",
                ShoePictureUrl = "image",

            };

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(shoeId));
                Assert.That(result.Name, Is.EqualTo(expectedResult.Name));
                Assert.That(result.Description, Is.EqualTo(expectedResult.Description));
            });
        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task GetDetailsForShoeAsyncShouldReturnNull(string shoeId)
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            var result = await shoeService.GetDetailsForShoeAsync(shoeId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task IsShoeExistsAsyncShouldReturnTrue()
        {
            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);


            var result = await shoeService.IsExistsAsync(shoeId);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task IsShoeExistsAsyncShouldReturnFalse(string shoeId)
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            var result = await shoeService.IsExistsAsync(shoeId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetShoeForEditAsyncShouldReturnCorrectResult()
        {
            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            var sizeMock = sizes.BuildMock();

            repoMock.Setup(r => r.GetAll<Size>())
                .Returns(sizeMock);


            var categoryMock = categories.BuildMock();

            repoMock.Setup(r => r.GetAll<Category>())
                .Returns(categoryMock);


            var shoe = new ShoeFormModel()
            {
                Name = "Test Shoe",
                Description = "test descritpion for shoe",
                Price = "13.3",

            };

            shoe.Categories = new List<CategoryViewModel>()
            {
                new CategoryViewModel()
                {
                    Id = "08851195-d523-418f-b272-37c3d91544df",
                    Name = "Sports"
                },
                new CategoryViewModel()
                {
                    Id = "22ef8689-c7fa-4315-8505-377ece4c16ff",
                    Name = "Men"
                },
                new CategoryViewModel()
                {
                    Id = "2f22398c-dc3c-4d81-9a56-19a46252210c",
                    Name = "Women"
                },
                new CategoryViewModel()
                {
                    Id = "4874581a-3b02-41e0-bdca-07cf40545f88",
                    Name = "Kids"
                },
            };

            shoe.Sizes = new List<SizeViewModel>()
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
                new SizeViewModel()
                {
                    Id = 5,
                    Number = 44
                }
            };

            var result = await shoeService.GetShoeForEditAsync(shoeId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(shoe.Name));
                Assert.That(result.Description, Is.EqualTo(shoe.Description));
                Assert.That(result.Sizes.ToList()[0].Id, Is.EqualTo(shoe.Sizes.ToList()[0].Id));
                Assert.That(result.Categories.ToList()[0].Id, Is.EqualTo(shoe.Categories.ToList()[0].Id));
            });
        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task GetShoeForEditAsyncShouldNull(string shoeId)
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            var sizeMock = sizes.BuildMock();

            repoMock.Setup(r => r.GetAll<Size>())
                .Returns(sizeMock);


            var categoryMock = categories.BuildMock();

            repoMock.Setup(r => r.GetAll<Category>())
                .Returns(categoryMock);

            var result = await shoeService.GetShoeForEditAsync(shoeId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetShoeForOrderAsyncShouldReturnCorrectResult()
        {
            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var expectedResult = new OrderShoeViewModel()
            {
                Id = shoeId,
                Name = "Test Shoe",
                Description = "test descritpion for shoe",
                Price = 13.3m,
                Size = 42,
                ShoeImageUrl = "image",
                IsEnabled = true,
            };

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);


            var result = await shoeService.GetShoeForOrderAsync(shoeId, 42);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(expectedResult.Id));
                Assert.That(result.Name, Is.EqualTo(expectedResult.Name));
                Assert.That(result.Description, Is.EqualTo(expectedResult.Description));
            });

        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task GetShoeForOrderAsyncShouldReturnNull(string shoeId)
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            var result = await shoeService.GetShoeForOrderAsync(shoeId, 42);

            Assert.That(result,Is.Null);
        }

        [Test]
        public async Task EditShoeAsyncShouldEditShoeCorrectly()
        {
            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var shoe = new ShoeFormModel()
            {
                Name = "Test Shoe",
                Description = "test descritpion for shoe",
                Price = "13",
                SizeIds = new List<int>() { 1, 2, 3 },
                CategoryId = "08851195-d523-418f-b272-37c3d91544df"

            };
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
                .Returns(shoesMock);

            await shoeService.EditShoeAsync(shoe, shoeId);

            repoMock.Setup(r => r.GetByIdAsync<Shoe>(shoeId))
                  .ReturnsAsync(shoesMock.FirstOrDefault(o => o.Id == shoeId));

            var result = await repoMock.Object.GetByIdAsync<Shoe>( shoeId);

            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(shoe.Name));
                Assert.That(result.Description, Is.EqualTo(shoe.Description));
                Assert.That(result.CategoryId, Is.EqualTo(shoe.CategoryId));
            });
        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task EditShoeAsyncShouldDoNothing(string shoeId)
        {
            var shoe = new ShoeFormModel()
            {
                Name = "Test Shoe",
                Description = "test descritpion for shoe",
                Price = "13",
                SizeIds = new List<int>() { 1, 2, 3 },
                CategoryId = "08851195-d523-418f-b272-37c3d91544df"

            };

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
               .Returns(shoesMock);

            await shoeService.EditShoeAsync(shoe, shoeId);

            repoMock.Setup(r => r.GetByIdAsync<Shoe>(shoeId))
                  .ReturnsAsync(shoesMock.FirstOrDefault(o => o.Id == shoeId));

            var result = await repoMock.Object.GetByIdAsync<Shoe>(shoeId);

            Assert.That(result, Is.Null);


        }

        [Test]
        public async Task GetShoeForDeleteAsyncShouldReturnCorrectResult()
        {
            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
               .Returns(shoesMock);

            var result = await shoeService.GetShoeForDeleteAsync(shoeId);

            PreDeleteShoeViewModel shoe = new PreDeleteShoeViewModel()
            {
                Id = shoeId,
                Name = "Test Shoe",
                Category = "Sports",
                Description = "test descritpion for shoe",
                Price = 13.3m,
                ShoePictureUrl = "image"
            };

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(shoeId));
                Assert.That(result.Name, Is.EqualTo(shoe.Name));
                Assert.That(result.Category, Is.EqualTo(shoe.Category));
            });
        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task GetShoeForDeleteAsyncShouldReturnNull(string shoeId)
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
               .Returns(shoesMock);

            var result = await shoeService.GetShoeForDeleteAsync(shoeId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteShoeAsyncShouldDeleteShoe()
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
               .Returns(shoesMock);

            string shoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4";

            

            await shoeService.DeleteShoeAsync(shoeId);

            repoMock.Setup(r => r.GetByIdAsync<Shoe>(shoeId))
                 .ReturnsAsync(shoesMock.Where(s => s.IsActive ).FirstOrDefault(o => o.Id == shoeId));

            var result = await repoMock.Object.GetByIdAsync<Shoe>(shoeId);

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase("id")]
        [TestCase("name")]
        [TestCase("description")]
        public async Task DeleteShoeAsyncShouldDoNothing(string shoeId)
        {
            var shoesMock = shoes.BuildMock();

            repoMock.Setup(r => r.GetAll<Shoe>())
               .Returns(shoesMock);
            await shoeService.DeleteShoeAsync(shoeId);

            

            repoMock.Setup(r => r.GetByIdAsync<Shoe>(shoeId))
                 .ReturnsAsync(shoesMock.Where(s => s.IsActive == false).FirstOrDefault(o => o.Id == shoeId));

            var result = await repoMock.Object.GetByIdAsync<Shoe>(shoeId);
            Assert.That(result, Is.Null);

        }

    }


}
