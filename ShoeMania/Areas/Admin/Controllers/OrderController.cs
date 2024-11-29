using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;

namespace ShoeMania.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
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
        public async Task<IActionResult> AdminOrders()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {

                var orders = await orderService.GetAllOrdersAsync();

                return View("All", orders);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
