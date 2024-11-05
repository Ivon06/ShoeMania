using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data
{
	public class ShoeManiaDbContext : IdentityDbContext<User>
	{
		private bool seedDb;

		public ShoeManiaDbContext(DbContextOptions<ShoeManiaDbContext> options, bool seedDb = true)
			: base(options)
		{

			if (this.Database.IsRelational())
			{
				this.Database.Migrate();
			}
			else
			{
				this.Database.EnsureCreated();
			}

			this.seedDb = seedDb;

		}
		public DbSet<Category> Category { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;
		public DbSet<OrderShoe> OrdersShoes { get; set; } = null!;
		public DbSet<Payment> Payments { get; set; } = null!;
		public DbSet<Shoe> Shoes { get; set; } = null!;

		public DbSet<SizeShoe> SizeShoes { get; set; } = null!;

		public DbSet<Size> Sizes { get; set; } = null!;
		public DbSet<Customer> Customers { get; set; } = null!;

		public DbSet<DeliveryOffice> DeliveryOffices { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<OrderShoe>()
				.HasKey(os => new { os.OrderId, os.ShoeId });
			builder.Entity<SizeShoe>()
				.HasKey(os => new { os.SizeId, os.ShoeId });

			base.OnModelCreating(builder);
		}
	}
}
