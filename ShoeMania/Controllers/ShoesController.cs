using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Shoes;
using ShoeMania.Extensions;
using static ShoeMania.Common.NotificationConstants;

namespace ShoeMania.Controllers
{
    public class ShoesController : BaseController
    {
        private readonly IShoeService shoeService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly ISizeService sizeService;

        public ShoesController(IShoeService shoeService, IUserService userService, ICategoryService categoryService, ISizeService sizeService)
        {
            this.shoeService = shoeService;
            this.userService = userService;
            this.categoryService = categoryService;
            this.sizeService = sizeService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All(ShoesQueryModel model)
        {
            var userId = User.GetId();
            bool isCustomer = await userService.IsCustomerAsync(userId!);

            if (!isCustomer && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                AllShoesFilteredAndPaged serviceModel = await shoeService.GetAllShoesFilteredAndPagedAsync(model);
                model.TotalShoes = serviceModel.TotalShoes;
                model.Shoes = serviceModel.Shoes.ToList();
                model.Categories = await categoryService.GetAllCategoryNamesAsync();

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            ShoeFormModel model = new ShoeFormModel();
            model.Categories = await categoryService.GetAllCategoriesAsync();
            model.Sizes = await sizeService.GetAllSizesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShoeFormModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.Sizes = await sizeService.GetAllSizesAsync();

                return View(model);
            }

            string shoeId = await shoeService.AddAsync(model);

            await sizeService.AddSizesToShoeAsync(model.SizeIds, shoeId);

            TempData[SuccessMessage] = "Successfuly added shoe";

            return RedirectToAction("All");


        }

        [HttpGet]
        public async Task<IActionResult> Edit(string shoeId)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData[ErrorMessage] = "Your should be admin to edit shoe";
                return RedirectToAction("All");
            }

            bool isExists = await shoeService.IsExistsAsync(shoeId);

            if (!isExists)
            {
                TempData[ErrorMessage] = "This shoe does not exist";
                return RedirectToAction("All");
            }

            try
            {
                var shoe = await shoeService.GetShoeForEditAsync(shoeId);

                return View(shoe);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string shoeId, ShoeFormModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData[ErrorMessage] = "Your should be admin to edit shoe";
                return RedirectToAction("All");
            }

            bool isExists = await shoeService.IsExistsAsync(shoeId);

            if (!isExists)
            {
                TempData[ErrorMessage] = "This shoe does not exist";
                return RedirectToAction("All");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }



            await shoeService.EditShoeAsync(model, shoeId);

            TempData[SuccessMessage] = "Successfully edited shoe";

            return RedirectToAction("All");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string shoeId)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData[ErrorMessage] = "Your should be admin to edit shoe";
                return RedirectToAction("All");
            }

            bool isExists = await shoeService.IsExistsAsync(shoeId);

            if (!isExists)
            {
                TempData[ErrorMessage] = "This shoe does not exist";
                return RedirectToAction("All");
            }

            var shoe = await shoeService.GetShoeForDeleteAsync(shoeId);

            TempData[WarningMessage] = "If you press the button delete you will delete this shoe";
            return View(shoe);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(string shoeId, PreDeleteShoeViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData[ErrorMessage] = "Your should be admin to edit shoe";
                return RedirectToAction("All");
            }

            bool isExists = await shoeService.IsExistsAsync(shoeId);

            if (!isExists)
            {
                TempData[ErrorMessage] = "This shoe does not exist";
                return RedirectToAction("All");
            }

            await shoeService.DeleteShoeAsync(shoeId);

            TempData[SuccessMessage] = "Successfully delete shoe";

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            bool isExists = await shoeService.IsExistsAsync(id);
            if (!isExists)
            {
                return RedirectToAction("All");
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = await shoeService.GetDetailsForShoeAsync(id);

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string? userName = User.GetUsername();
            var cartShoes = shoeService.GetCartShoes(userName!);

            return await Task.Run(() => View(cartShoes));

        }

        public async Task<IActionResult> AddToCart(DetailsShoeViewModel model)
        {
            string shoeId = model.Id;
            bool isExists = await shoeService.IsExistsAsync(shoeId);

            if (!isExists)
            {
                return RedirectToAction("All");
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            string userName = User.GetUsername()!;
            await shoeService.AddShoeToCart(userName, shoeId, model.Size);

            TempData[SuccessMessage] = "Successfully added to cart";

            return RedirectToAction("Cart");


        }
    }
}
