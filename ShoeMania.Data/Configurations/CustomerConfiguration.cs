using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>

    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(GetCustomers());
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id = "a3bd28c7-f6f8-4eb8-9ca3-d3539faf427e",
                    UserId = "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3"
                }
            };
        }
    }
}
