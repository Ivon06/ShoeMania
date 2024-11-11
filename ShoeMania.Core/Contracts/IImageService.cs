using Microsoft.AspNetCore.Http;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IImageService
    {
        Task<string> UploadImageToUser(IFormFile image, string folderName, User user);
        Task<string> UploadImageToShoe(IFormFile image, string folderName, Shoe shoe);
    }
}
