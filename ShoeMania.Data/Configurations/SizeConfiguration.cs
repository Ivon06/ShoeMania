using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeMania.Data.Models;

namespace ShoeMania.Data.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasData(GetSizes());
        }

        private List<Size> GetSizes()
        {
            var sizes = new List<Size>()
            {
                new Size()
                {
                    Id = 1,
                    Number = 36,
                },
                new Size()
                {
                    Id = 2,
                    Number = 37
                },
                new Size()
                {
                    Id = 3,
                    Number = 38,
                },
                new Size()
                {
                    Id = 4,
                    Number = 39
                },
                new Size()
                {
                    Id = 5,
                    Number = 40,
                },
                new Size()
                {
                    Id = 6,
                    Number = 41
                },
                new Size()
                {
                    Id = 7,
                    Number = 42,
                },
                new Size()
                {
                    Id = 8,
                    Number = 43
                },
                new Size()
                {
                    Id = 9,
                    Number = 44
                },
                new Size()
                {
                    Id = 10,
                    Number = 45
                }

            };

            return sizes;
        }
    }
}
