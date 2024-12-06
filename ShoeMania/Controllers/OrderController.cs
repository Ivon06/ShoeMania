using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Core.ViewModels.Order;
using ShoeMania.Data.Models.Enums;
using ShoeMania.Extensions;
using static ShoeMania.Common.NotificationConstants;


namespace ShoeMania.Controllers
{
    public class OrderController : BaseController
    {
		private string url = "https://api.speedy.bg/v1/location/office";
	

        private readonly IUserService userService;
        private readonly IShoeService shoeService;
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
        private readonly IPaymentService paymentService;
        private readonly IDeliveryOfficeService deliveryOfficeService;

        public OrderController(IUserService userService, IShoeService shoeService, ICustomerService customerService, IOrderService orderService, IPaymentService paymentService, IDeliveryOfficeService deliveryOfficeService)
        {
            this.userService = userService;
            this.shoeService = shoeService;
            this.customerService = customerService;
            this.orderService = orderService;
            this.paymentService = paymentService;
            this.deliveryOfficeService = deliveryOfficeService;
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

            model.Offices = await deliveryOfficeService.GetDeliveryOfficeListAsync(); ;

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

		public async Task<IActionResult> UserOrders()
		{
			if (!User.IsInRole("Customer"))
			{
				return RedirectToAction("Index", "Home");
			}

			string? customerId = await customerService.GetCustomerIdByUserIdAsync(User.GetId()!);

			try
			{
				var orders = await orderService.GetCustomerOrdersAsync(customerId!);
				return View("All", orders);

			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Home");
			}

		}
		

		


	}
}
