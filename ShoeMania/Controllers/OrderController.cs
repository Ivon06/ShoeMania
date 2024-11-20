using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Order;
using ShoeMania.Extensions;
using static ShoeMania.Common.NotificationConstants;


namespace ShoeMania.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IUserService userService;
        private readonly IShoeService shoeService;
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
       
        public OrderController(IUserService userService, IShoeService shoeService, ICustomerService customerService, IOrderService orderService )
        {
            this.userService = userService;
            this.shoeService = shoeService;
            this.customerService = customerService;
            this.orderService = orderService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Order(string paymentId)
        {
            var userId = User.GetId();
            bool isCustomer = await userService.IsCustomerAsync(userId!);

            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!isCustomer && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            string? userName = User.GetUsername();
            var cartShoes = shoeService.GetCartShoes(userName!);

            OrderFormModel model = new OrderFormModel()
            {
                PaymentId = paymentId,
                Shoes = cartShoes!
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Order(OrderFormModel model)
        {
            string userName = User.GetUsername()!;

            if (!ModelState.IsValid)
            {
                model.Shoes = shoeService.GetCartShoes(userName!)!;
                return View(model);
            }

            try
            {

                model.Shoes = shoeService.GetCartShoes(userName!)!;

                string? customerId = await customerService.GetCustomerIdByUserIdAsync(User.GetId()!);

                string orderId = await orderService.CreateOrderAsync(model, customerId!);

                await paymentService.AddOrderToPaymentAsync(model.PaymentId, orderId);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.Clear();
            TempData[SuccessMessage] = "Successful order";
            return RedirectToAction("UserOrders");

        }

    }
}
