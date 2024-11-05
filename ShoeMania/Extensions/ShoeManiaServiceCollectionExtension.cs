using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Migrations;
using ShoeMania.Data.Repository;

namespace ShoeMania.Extensions
{
	public static class ShoeManiaServiceCollectionExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddScoped<IRepository, Repository>();

			return services;
		}
	}
}
