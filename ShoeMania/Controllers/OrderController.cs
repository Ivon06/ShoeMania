using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
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

        public OrderController(IUserService userService, IShoeService shoeService, ICustomerService customerService, IOrderService orderService, IPaymentService paymentService)
        {
            this.userService = userService;
            this.shoeService = shoeService;
            this.customerService = customerService;
            this.orderService = orderService;
            this.paymentService = paymentService;
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
		

		[HttpGet]
		public async Task<IActionResult> Accept(string orderId)
		{
			if (!User.IsInRole("Admin"))
			{
				return RedirectToAction("UserOrders");
			}

			bool isOrderExists = await orderService.IsOrderExistsAsync(orderId);
			if (!isOrderExists)
			{
				return RedirectToAction("AdminOrders");
			}

			var order = await orderService.GetOrderByIdAsync(orderId);

			return View(order);
		}

		[HttpPost]
		public async Task<IActionResult> Accept(AcceptOrderFormModel model)
		{
			string orderId = model.Id;

			if (!User.IsInRole("Admin"))
			{
				return RedirectToAction("UserOrders");
			}

			bool isOrderExists = await orderService.IsOrderExistsAsync(orderId);
			if (!isOrderExists)
			{
				return RedirectToAction("AdminOrders");
			}

			if (model.DeliveryTime.Date < DateTime.Parse(model.OrderTime).Date)
			{
				ModelState.AddModelError(nameof(model.DeliveryTime), "Delivery date should be after order date");
			}
			if (model.DeliveryTime < DateTime.Parse(model.OrderTime))
			{
				ModelState.AddModelError(nameof(model.DeliveryTime), "Delivery time should be after order time");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await orderService.AddDeliveryTimeForOrderAsync(model);
			await orderService.ChangeStatusOrderAsync(orderId, OrderStatusEnum.Confirmed.ToString());
			TempData[SuccessMessage] = "Order is accepted";

			return RedirectToAction("AdminOrders");


		}

		[HttpGet]
		public async Task<IActionResult> Edit(string orderId)
		{
			if (!User.IsInRole("Admin"))
			{
				return RedirectToAction("UserOrders");
			}

			bool isOrderExists = await orderService.IsOrderExistsAsync(orderId);
			if (!isOrderExists)
			{
				return RedirectToAction("AdminOrders");
			}

			var order = await orderService.GetOrderForEditByIdAsync(orderId);

			return View(order);

		}

		[HttpPost]
		public async Task<IActionResult> Edit(string orderId, AcceptOrderFormModel model)
		{
			if (!User.IsInRole("Admin"))
			{
				return RedirectToAction("UserOrders");
			}

			bool isOrderExists = await orderService.IsOrderExistsAsync(orderId);
			if (!isOrderExists)
			{
				return RedirectToAction("AdminOrders");
			}

			await orderService.EditDeliveryTimeForOrderAsync(model);
			TempData[SuccessMessage] = "Successfully edited order";

			return RedirectToAction("AdminOrders");


		}
		public async Task<IActionResult> Send(string orderId)
		{
			if (!User.IsInRole("Admin"))
			{
				return RedirectToAction("UserOrders");
			}

			bool isOrderExists = await orderService.IsOrderExistsAsync(orderId);
			if (!isOrderExists)
			{
				return RedirectToAction("AdminOrders");
			}

			await orderService.ChangeStatusOrderAsync(orderId, OrderStatusEnum.Send.ToString());
			TempData[SuccessMessage] = "Order is sent";

			return RedirectToAction("AdminOrders");
		}

		public async Task<IActionResult> DeliverOrder(string orderId)
		{
			if (!User.IsInRole("Admin"))
			{
				return RedirectToAction("UserOrders");
			}

			bool isOrderExists = await orderService.IsOrderExistsAsync(orderId);
			if (!isOrderExists)
			{
				return RedirectToAction("AdminOrders");
			}

			await orderService.ChangeStatusOrderAsync(orderId, OrderStatusEnum.Delivered.ToString());
			TempData[SuccessMessage] = "The order is already delivered";

			return RedirectToAction("AdminOrders");
		}


	}
}
