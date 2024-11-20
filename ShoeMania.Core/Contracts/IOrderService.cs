using ShoeMania.Core.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(OrderFormModel model, string cutomerId);

        Task<List<OrderViewModel>> GetCustomerOrdersAsync(string cutomerId);
        Task<List<OrderViewModel>> GetAllOrdersAsync();

        Task<bool> IsOrderExistsAsync(string orderId);

        Task ChangeStatusOrderAsync(string orderId, string status);

        Task AddDeliveryTimeForOrderAsync(AcceptOrderFormModel model);

        Task<AcceptOrderFormModel> GetOrderByIdAsync(string orderId);
        Task<AcceptOrderFormModel> GetOrderForEditByIdAsync(string orderId);

        Task EditDeliveryTimeForOrderAsync(AcceptOrderFormModel model);
    }
}
