using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Repository
{
	public interface IRepository
	{
	IQueryable<T> GetAll<T>() where T : class;

		IEnumerable<T> All<T>() where T : class;

        Task<IQueryable<T>> AllAsync<T>() where T : class;

        T? GetById<T>(string id) where T : class;

		Task<T?> GetByIdAsync<T>(string id) where T : class;

		void Add<T>(T entity) where T : class;

		Task AddAsync<T>(T entity) where T : class;

		void AddRange<T>(IEnumerable<T> entity) where T : class;

		Task AddRangeAsync<T>(IEnumerable<T> entity) where T : class;

		void Update<T>(string id, T entity) where T : class;

		void RemoveRange<T>(IEnumerable<T> entity)	where T : class;

		Task DeleteAsync<T>(string id) where T : class;

		void Delete<T>(T entity) where T : class;

		Task<int> SaveChangesAsync();
	}
}
