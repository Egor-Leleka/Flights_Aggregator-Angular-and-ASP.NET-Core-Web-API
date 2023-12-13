using System.Linq.Expressions;

namespace FlightsAggregator.DataAccess.Repositories.IRepositories
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
		T Get(Expression<Func<T, bool>> filter, bool tracked = false);
		void Add(T entity);
		void Remove(T entity);
	}
}
