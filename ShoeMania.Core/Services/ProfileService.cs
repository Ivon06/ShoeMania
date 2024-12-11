using Microsoft.EntityFrameworkCore;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.ViewModels.Profile;
using ShoeMania.Data.Models;
using ShoeMania.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository repo;
        private readonly IImageService imageService;

        public ProfileService( IImageService imageService, IRepository repo)
        {
          
            this.imageService = imageService;
            this.repo = repo;
        }

        public async Task EditProfileAsync(string userId, EditProfileViewModel model)
        {
            var user = await repo.GetAll<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Address = model.Address;
            user.Country = model.Country;
            user.City = model.City;
            user.PhoneNumber = model.Phone;

            if (model.ProfilePicture != null)
            {
                user.ProfilePictureUrl = await imageService.UploadImageToUser(model.ProfilePicture, "ShoeManiaProject", user);
            }

            await repo.SaveChangesAsync();


        }

        public async Task<ProfileViewModel?> GetProfileAsync(string userId)
        {
            var profile = await repo.GetAll<Customer>()
                .Include(c => c.User)
                .Where(u => u.UserId == userId)
                .Select(c => new ProfileViewModel()
                {
                    Id = c.UserId,
                    Name = $"{c.User.FirstName} {c.User.LastName}",
                    Email = c.User.Email,
                    City = c.User.City,
                    Country = c.User.Country,
                    Address = c.User.Address,
                    PhoneNumber = c.User.PhoneNumber,
                    ProfilePictureUrl = c.User.ProfilePictureUrl

                })
                .FirstOrDefaultAsync();

            return profile;
        }

        public async Task<EditProfileViewModel> GetProfileForEditAsync(string userId)
        {
            var profile = await repo.GetAll<User>()
                .Where(u => u.Id == userId)
                .Select(u => new EditProfileViewModel()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    City = u.City,
                    Country = u.Country,
                    Address = u.Address,
                    Phone = u.PhoneNumber,
                    ProfilePictureUrl = u.ProfilePictureUrl
                })
                .FirstOrDefaultAsync();

            if (profile == null)
            {
                return null;
            }

            return profile!;
        }
    }
}
