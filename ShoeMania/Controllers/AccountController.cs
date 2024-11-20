using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Account;
using ShoeMania.Data.Models;
using static ShoeMania.Common.NotificationConstants;
namespace ShoeMania.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IImageService imageService;
        private readonly IUserService userService;
        private readonly ICustomerService customerService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IImageService imageService, IUserService userService, ICustomerService customerService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.imageService = imageService;
            this.userService = userService;
            this.customerService = customerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return await Task.Run(() => View(model));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await userService.ExistsByEmailAsync(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "There is already registered user with this email");
            }

            if (await userService.ExistsByPhoneAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "There is already registered user with this phone number");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
                Country = model.Country,
                City = model.City,
                Address = model.Address

            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");

                await customerService.Create(user.Id);

                if (model.ProfilePicture != null)
                {
                    user.ProfilePictureUrl = await imageService.UploadImageToUser(model.ProfilePicture, "ShoeManiaProject", user);

                    await userManager.UpdateAsync(user);
                }

                await signInManager.SignInAsync(user, isPersistent: false);

                TempData[SuccessMessage] = "Successful register";

                return RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                if (user.IsActive)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        TempData[SuccessMessage] = "Successful login";
                        return RedirectToAction("Index", "Home");
                    }

                }
            }

            ModelState.AddModelError(nameof(model.Email), "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
