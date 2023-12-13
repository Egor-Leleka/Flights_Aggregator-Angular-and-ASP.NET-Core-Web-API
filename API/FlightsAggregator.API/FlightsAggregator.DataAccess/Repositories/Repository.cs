using FlightsAggregator.DataAccess.Data;
using FlightsAggregator.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlightsAggregator.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private ApplicationDbContext _repository;
		private DbSet<T> _repositorySet;

		public Repository(ApplicationDbContext repository)
        {
			_repository = repository;
			_repositorySet = _repository.Set<T>();
		}

        public void Add(T entity)
		{
			_repositorySet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, bool tracked = false)
		{
			IQueryable<T> query;

			if (tracked)
				query = _repositorySet;
			else
				query = _repositorySet.AsNoTracking();

			query = query.Where(filter);
			
			return query.FirstOrDefault();
		}

		public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null)
		{
			IQueryable<T> query = _repositorySet;

			if (filter != null)
				query = query.Where(filter);

			return query.AsQueryable();
		}

		public void Remove(T entity)
		{
			_repositorySet.Remove(entity);
		}
	}
}
