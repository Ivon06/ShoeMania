using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Configurations
{
    public class DeliveryOfficeConfiguration : IEntityTypeConfiguration<DeliveryOffice>
    {
        string jsonString = File.ReadAllText("../ShoeMania.Data/Configurations/addresses.json");
        public void Configure(EntityTypeBuilder<DeliveryOffice> builder)
        {
            builder.HasData(Deserializer.Deserializer.ImportAddresses(jsonString));
        }
    }
}
