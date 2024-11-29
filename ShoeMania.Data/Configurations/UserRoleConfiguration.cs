using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(GetAll());
        }

        private List<IdentityUserRole<string>> GetAll()
        {
            List<IdentityUserRole<string>> list =  new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    RoleId = "835c8458-e8b7-493f-9c13-67bfcd7316a3",
                    UserId = "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "78374b9b-5158-4aff-8626-d088a02d79e1",
                    UserId = "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3"
                },
            };

            return list;
        }
    }
}
