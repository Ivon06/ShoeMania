using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Order;
using ShoeMania.Data.Models.Enums;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeMania.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace ShoeMania.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repo;

       
        public OrderService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddDeliveryTimeForOrderAsync(AcceptOrderFormModel model)
        {
            var order = await repo.GetAll<Order>().FirstOrDefaultAsync(o => o.Id == model.Id);
            if (order == null)
            {
                return;
            }

            order.DeliveryTime = model.DeliveryTime;
            await repo.SaveChangesAsync();
        }

        public async Task ChangeStatusOrderAsync(string orderId, string status)
        {
            var order = await repo.GetAll<Order>().FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return;
            }

            order.Status = status;

            await repo.SaveChangesAsync();
        }

        public async Task<string> CreateOrderAsync(OrderFormModel model, string customerId)
        {
            var order = new Order()
            {
                CustomerId = customerId,
                Status = OrderStatusEnum.Waiting.ToString(),
                OrderTime = DateTime.Now,
                DeliveryOfficeId = model.DeliveryOfficeId,
                Price = (decimal)(model.Shoes.Select(d => d.Price).Sum() +
                0.05m * model.Shoes.Select(d => d.Price).Sum() + 5)!,
                PaymentId = model.PaymentId,
               

            };

            List<OrderShoe> orderShoes = new();
            foreach (var shoe in model.Shoes)
            {
                OrderShoe sho = new OrderShoe()
                {
                    OrderId = order.Id,
                    ShoeId = shoe.Id,
                    ShoeSize = (int)shoe.Size!,
                };

                orderShoes.Add(sho);
            }

            order.OrderShoe = orderShoes;

            await repo.AddAsync(order);

            await repo.SaveChangesAsync();

            return order.Id;


        }

        public async Task EditDeliveryTimeForOrderAsync(AcceptOrderFormModel model)
        {
            var order = await repo.GetAll<Order>().FirstOrDefaultAsync(o => o.Id == model.Id);

            if (order == null)
            {
                return;
            }

            order.DeliveryTime = model.DeliveryTime;

            await repo.SaveChangesAsync();

        }

        public async Task<List<OrderViewModel>> GetAllOrdersAsync()
        {
            var orders = await repo.GetAll<Order>()
                .Include(o => o.OrderShoe)
                .Include(o => o.Customer)
                .Include(o => o.Customer.User)
                .Include(o=>o.DeliveryOffice)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    DeliveryAddress = o.DeliveryOffice.Address,
                    DeliveryTime = o.DeliveryTime.HasValue
                        ? $"{o.DeliveryTime.Value.ToString("dddd, dd MMMM yyyy")}"
                        : string.Empty,
                    OrderTime = $"{o.OrderTime.ToString("dddd, dd MMMM yyyy")}",
                    Status = o.Status,
                    CustomerPhoneNumber = o.Customer.User.PhoneNumber,
                    Shoes = o.OrderShoe.Select(d => new OrderedShoeInfo()
                    {
                        Name = d.Shoe.Name,
                        Size = d.ShoeSize
                    }).ToList(),
                    Price = o.Price,

                })
                .ToListAsync();

            return orders;
        }

        public async Task<List<OrderViewModel>> GetCustomerOrdersAsync(string cutomerId)
        {
            var orders = await repo.GetAll<Order>()
                .Where(o => o.CustomerId == cutomerId && o.Status != OrderStatusEnum.Delivered.ToString())
                .Include(o => o.OrderShoe)
                .Include(o => o.Customer.User)
                .Include(o => o.DeliveryOffice)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    DeliveryAddress = o.DeliveryOffice.Address,
                    DeliveryTime = o.DeliveryTime.HasValue
                        ? o.DeliveryTime.Value.ToString("dddd, dd MMMM yyyy")
                        : string.Empty,
                    OrderTime = o.OrderTime.ToString("dddd, dd MMMM yyyy"),
                    Status = o.Status,
                    CustomerPhoneNumber = o.Customer.User.PhoneNumber,
                    Shoes = o.OrderShoe.Select(d => new OrderedShoeInfo()
                    {
                        Name = d.Shoe.Name,
                        Size = d.ShoeSize
                    }).ToList(),
                    Price = o.Price,

                })
                .ToListAsync();

            return orders;
        }

        public async Task<AcceptOrderFormModel> GetOrderByIdAsync(string orderId)
        {
            var order = await repo.GetAll<Order>()
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderShoe)
                .Include(o => o.Customer.User)
                .Include(o => o.DeliveryOffice)
                .Select(o => new AcceptOrderFormModel()
                {
                    Id = o.Id,
                    CustomerName = $"{o.Customer.User.FirstName} {o.Customer.User.LastName}",
                    OrderTime = o.OrderTime.ToString("dddd, dd MMMM yyyy"),
                    Status = o.Status,
                    DeliveryAddress = o.DeliveryOffice.Address,
                    Shoes = o.OrderShoe.Select(s => new OrderedShoeInfo()
                    {
                        Name = s.Shoe.Name,
                        Size = s.ShoeSize
                    })
                    .ToList(),
                    Price = o.Price,

                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }


            return order!;
        }

        public async Task<AcceptOrderFormModel> GetOrderForEditByIdAsync(string orderId)
        {
            var order = await repo.GetAll<Order>()
               .Where(o => o.Id == orderId)
               .Include(o => o.OrderShoe)
               .Include(o => o.Customer.User)
               .Include(o => o.DeliveryOffice)
               .Select(o => new AcceptOrderFormModel()
               {
                   Id = o.Id,
                   CustomerName = $"{o.Customer.User.FirstName} {o.Customer.User.LastName}",
                   OrderTime = o.OrderTime.ToString("dddd, dd MMMM yyyy"),
                   Status = o.Status,
                   DeliveryAddress = o.DeliveryOffice.Address,
                   DeliveryTime = (DateTime)o.DeliveryTime,
                   Shoes = o.OrderShoe.Select(s => new OrderedShoeInfo()
                   {
                       Name = s.Shoe.Name,
                       Size = s.ShoeSize
                   })
                   .ToList(),
                   Price = o.Price,

               })
               .FirstOrDefaultAsync();



            return order!;
        }

        public async Task<bool> IsOrderExistsAsync(string orderId)
        {
            var isExists = await repo.GetAll<Order>()
                .AnyAsync(o => o.Id == orderId);

            return isExists;
        }
    }
}
