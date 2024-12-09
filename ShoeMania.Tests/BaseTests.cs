using ShoeMania.Data;
using ShoeMania.Data.Models;
using ShoeMania.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Tests
{
    [TestFixture]
    public class BaseTests
    {
        protected List<Category> categories;
        protected List<Customer> customers;
        protected List<Order> orders;
        protected List<DeliveryOffice> deliveryOffices;
        protected List<OrderShoe> orderShoes;
        protected List<Payment> payments;
        protected List<Shoe> shoes;
        protected List<Size> sizes;
        protected List<SizeShoe> sizeShoes;
        protected List<User> users;

        protected ShoeManiaDbContext context;

        [SetUp]
        public async Task SetUpBase()
        {
            this.context = DbMock.Instance;
            await this.SeedData();
        }

        public async Task SeedData()
        {
            var categories = new List<Category>()
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

            await context.AddRangeAsync(categories);


            var shoe = new Shoe()
            {
                Id = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4",
                Name = "Test Shoe",
                CategoryId = "08851195-d523-418f-b272-37c3d91544df",
                Description = "test descritpion for shoe",
                Price = 13.3m,
                ShoeUrlImage = "image",
                IsActive = true
            };

            await context.Shoes.AddAsync(shoe);


            var sizes = new List<Size>()
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

            await context.Sizes.AddRangeAsync(sizes);

            var sizesShoe = new List<SizeShoe>()
            {
                new SizeShoe()
                {
                    SizeId = 1,
                    ShoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4"
                },
                new SizeShoe()
                {
                    SizeId = 2,
                    ShoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4"
                },
                new SizeShoe()
                {
                    SizeId = 3,
                    ShoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4"
                },
                new SizeShoe()
                {
                    SizeId = 4,
                    ShoeId = "9cb6c9ed-4bdc-4447-8aff-df120cc658a4"
                },

            };

           await  context.SizeShoes.AddRangeAsync(sizesShoe);

            var order = new Order()
            {
                Id = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                Status = "Waiting",
                CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
                OrderTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(1),
                DeliveryOfficeId = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                Price = 120.0m,

            };

            await context.Orders.AddRangeAsync(order);

            var payment = new Payment()
            {
                Id = "c88747fc-4915-4b69-8f91-7758f8635f8e",
                CustomerId = "d1d73a5e-f042-436f-bcca-24b5537988e8",
                OrderId = "7345f9f7-2e6f-4143-a728-d1d64a57e865",
                CardNumber = "0123456789101112",
                CardHolder = "Test Testov",
                ExpityDate = DateTime.Now.AddYears(2),
                SecurityCode = "8972"

            };

           await  context.Payments.AddAsync(payment);

            var office = new DeliveryOffice()
            {
                Id = "000d8e82-bda8-4c3a-abc3-34048f3b1bda",
                Name = "ВАРНА - ЛЕВСКИ (КВ.)",
                Address = "ул. СТУДЕНТСКА 4 ДО ВХ. Д"
            };

           await context.AddAsync(office);
        }



    }
}
