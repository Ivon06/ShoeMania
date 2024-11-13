using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Shoes;
using ShoeMania.Extensions;
using static ShoeMania.Common.NotificationConstants;

namespace ShoeMania.Controllers
{
    public class ShoesController : Controller
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
    }
}
