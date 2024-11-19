using Microsoft.AspNetCore.Mvc;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Profile;
using ShoeMania.Extensions;
using static ShoeMania.Common.NotificationConstants;

namespace ShoeMania.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IProfileService profileService;
        private readonly IUserService userService;

        public ProfileController(IProfileService profileService, IUserService userService)
        {
            this.profileService = profileService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile(string userId)
        {
            bool isExists = await userService.IsExistsByIdAsync(userId);
            if (!isExists)
            {
                return RedirectToAction("Index", "Home");
            }

            var profile = await profileService.GetProfileAsync(userId);
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isCustomer = await userService.IsCustomerAsync(userId);
            if (!isCustomer)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(profile);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            bool isExists = await userService.IsExistsByIdAsync(userId);
            if (!isExists)
            {
                return RedirectToAction("Index", "Home");
            }

            if (userId != User.GetId())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = await profileService.GetProfileForEditAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, EditProfileViewModel model)
        {
            bool isExists = await userService.IsExistsByIdAsync(userId);
            if (!isExists)
            {
                return RedirectToAction("Index", "Home");
            }
            if (userId != User.GetId())
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await profileService.EditProfileAsync(userId, model);

            TempData[SuccessMessage] = "Successfully edited profile";

            return RedirectToAction("MyProfile", new { userId });
        }

        
    }
}
