using MockQueryable;
using Moq;
using NUnit.Framework.Legacy;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Category;
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
    public class CategoryServiceTests
    {
        private Mock<IRepository> repoMock;
        private ICategoryService categoryService;

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

        [SetUp]
        public async Task SetUpBase()
        {
            repoMock = new Mock<IRepository>();
            categoryService = new CategoryService(repoMock.Object);
        }

        [Test]
        public async Task GetAllcategoriesAsync_ShouldReturnCorrect()
        {
            List<CategoryViewModel> expected = new List<CategoryViewModel>()
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

            IQueryable<Category> categs = categories.BuildMock();

            repoMock.Setup(r => r.GetAll<Category>())
                     .Returns(categs);

            var actual = await categoryService.GetAllCategoriesAsync();

            Assert.Multiple(() =>
            {
                CollectionAssert.IsNotEmpty(actual);
                Assert.That(actual[0].Name, Is.EqualTo(expected[0].Name));
            });

        }

        [Test]
        public async Task GetAllCategoryNameShouldReturnCurrectResult()
        {
            IQueryable<Category> categs = categories.BuildMock();

            repoMock.Setup(r => r.GetAll<Category>())
                .Returns(categs);

            var result = await categoryService.GetAllCategoryNamesAsync();
            var expectedResult = new List<string>() { "Sports", "Men", "Women", "Kids" };

            Assert.Multiple(() =>
            {
                CollectionAssert.IsNotEmpty(result);
                CollectionAssert.AreEqual(expectedResult, result);
            });
        }
    }
}
