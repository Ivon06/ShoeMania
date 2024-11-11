using Microsoft.AspNetCore.Http;
using ShoeMania.Core.Contracts;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeMania.Data.Repository;
using ShoeMania.Data.Models;
using CloudinaryDotNet.Actions;
using ShoeMania.Data;

namespace ShoeMania.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary cloudinary;
        private readonly ShoeManiaDbContext context;

        public ImageService(Cloudinary cloudinary, ShoeManiaDbContext context)
        {
            this.cloudinary = cloudinary;
            this.context = context;
        }
        public async Task<string> UploadImageToUser(IFormFile imageFile, string folderName, User user)
        {
            using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(user.Id, stream),
                Folder = folderName
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new InvalidOperationException(uploadResult.Error.Message);
            }

            user.ProfilePictureUrl = uploadResult.Url.ToString();

            return user.ProfilePictureUrl;
        }

        public async Task<string> UploadImageToShoe(IFormFile imageFile, string folderName, Shoe shoe)
        {
            using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(shoe.Id, stream),
                Folder = folderName
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new InvalidOperationException(uploadResult.Error.Message);
            }

            shoe.ShoeUrlImage = uploadResult.Url.ToString();

            return shoe.ShoeUrlImage;
        }
    }
}
