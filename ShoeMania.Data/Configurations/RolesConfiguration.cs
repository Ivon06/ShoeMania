using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(GetRoles());
        }

        private List<IdentityRole> GetRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "78374b9b-5158-4aff-8626-d088a02d79e1",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"

                },
                new IdentityRole()
                {
                    Id = "835c8458-e8b7-493f-9c13-67bfcd7316a3",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            };

            return roles;


        }
    }
}
