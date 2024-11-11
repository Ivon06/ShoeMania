using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IUserService
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByPhoneAsync(string phone);

        Task<bool> IsCustomerAsync(string userId);

        Task<bool> IsExistsByIdAsync(string id);

    }
}
