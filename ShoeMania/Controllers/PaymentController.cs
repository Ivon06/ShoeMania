using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Payment;
using ShoeMania.Extensions;
using static ShoeMania.Common.NotificationConstants;

namespace ShoeMania.Controllers
{
	public class PaymentController : Controller
	{
		private readonly IUserService userService;
		private readonly IPaymentService paymentService;
		private readonly ICustomerService customerService;

		public PaymentController(IUserService userService, IPaymentService paymentService, ICustomerService customerService)
		{
			this.userService = userService;
			this.paymentService = paymentService;
			this.customerService = customerService;
		}

		[HttpGet]
		public async Task<IActionResult> Payment()
		{
			var userId = User.GetId();
			bool isCustomer = await userService.IsCustomerAsync(userId!);

			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (!isCustomer && !User.IsInRole("Admin"))
			{
				return RedirectToAction("Index", "Home");
			}

			PaymentFormModel model = new PaymentFormModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Payment(PaymentFormModel model)
		{
			var userId = User.GetId();
			bool isCustomer = await userService.IsCustomerAsync(userId!);

			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (!User.IsInRole("Admin") && !isCustomer)
			{
				return RedirectToAction("Index", "Home");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			string? customerId = await customerService.GetCustomerIdByUserIdAsync(userId!);

			string paymentId = await paymentService.CreatePaymentAsync(model, customerId!);

			TempData[SuccessMessage] = "Successful payment";

			return RedirectToAction("Order", "Order", new { paymentId });
		}
	}
}
