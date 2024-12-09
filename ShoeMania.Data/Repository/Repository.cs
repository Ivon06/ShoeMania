using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Repository
{
    public class Repository : IRepository
	{ 
		private ShoeManiaDbContext dbContext;
		public Repository(ShoeManiaDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public DbSet<T> DbSet<T>() where T : class
		{
			return this.dbContext.Set<T>();
		}
		public void Add<T>(T entity) where T : class
		{
			DbSet<T>().Add(entity);
			dbContext.SaveChanges();
		}

		public async Task AddAsync<T>(T entity) where T : class
		{
			await DbSet<T>().AddAsync(entity);

		}

		public void AddRange<T>(IEnumerable<T> entity) where T : class
		{
			DbSet<T>().AddRange(entity);
		}

		public async Task AddRangeAsync<T>(IEnumerable<T> entity) where T : class
		{
			await DbSet<T>().AddRangeAsync(entity);
		}

		public async Task DeleteAsync<T>(string id) where T : class
		{
			T? entity = await GetByIdAsync<T>(id);

			Delete<T>(entity!);
		}

		public void Delete<T>(T entity) where T : class
		{
			EntityEntry entry = this.dbContext.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				this.DbSet<T>().Attach(entity);
			}

			entry.State = EntityState.Deleted;
		}

		public IQueryable<T> GetAll<T>() where T : class
		{
			return  DbSet<T>().AsQueryable();
		}

		public T? GetById<T>(string id) where T : class
		{
			return DbSet<T>().Find(id);
		}

		public async Task<T?> GetByIdAsync<T>(string id) where T : class
		{
			return await DbSet<T>().FindAsync(id);
		}

		public void Update<T>(string id, T entity) where T : class
		{
			this.DbSet<T>().Update(entity);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await this.dbContext.SaveChangesAsync();
		}

        public void RemoveRange<T>(IEnumerable<T> entity) where T : class
        {
            DbSet<T>().RemoveRange(entity);
        }

        public IEnumerable<T> All<T>() where T : class
        {
            return DbSet<T>().AsEnumerable();
        }

        public async Task<IQueryable<T>> AllAsync<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }
    }
}
