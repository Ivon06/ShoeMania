using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Migrations;
using ShoeMania.Core.Contracts;
using ShoeMania.Core.Services;
using ShoeMania.Data.Repository;

namespace ShoeMania.Extensions
{
	public static class ShoeManiaServiceCollectionExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddScoped<IRepository, Repository>();
			services.AddScoped<IImageService, ImageService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISizeService, SizeService>(); services.AddScoped<IShoeService,ShoeService>();

			return services;
		}
	}
}
