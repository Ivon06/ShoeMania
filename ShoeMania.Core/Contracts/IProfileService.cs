using ShoeMania.Core.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IProfileService
    {
        Task<ProfileViewModel?> GetProfileAsync(string userId);

        Task<EditProfileViewModel> GetProfileForEditAsync(string userId);

        Task EditProfileAsync(string userId, EditProfileViewModel model);

    }
}
