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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(GetCategories());
        }

        public ICollection<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = "asc3dde4-7fd9-4c33-add1-ef8l3c7kdm0c",
                    Name = "Mens"
                },
                new Category()
                {
                    Id = "8kd954a9-3od1-ldp1-a984-e91kshb22e23e",
                    Name = "Women"
                },
                new Category()
                {
                    Id = "2e92ejgd-95de-486a-a2qd-360ihtc3066d",
                    Name = "Sport"
                }
            };

            return categories;
        }
    }
}
