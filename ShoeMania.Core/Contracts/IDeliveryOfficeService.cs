using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IDeliveryOfficeService
    {
        Task<List<DeliveryOffice>> GetDeliveryOfficeListAsync();
    }
}
