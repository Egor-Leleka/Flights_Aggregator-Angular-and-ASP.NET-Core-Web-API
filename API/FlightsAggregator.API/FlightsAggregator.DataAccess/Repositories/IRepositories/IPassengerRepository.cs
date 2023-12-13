using FlightsAggregator.Models.Domain.ApplicationDbContext;

namespace FlightsAggregator.DataAccess.Repositories.IRepositories
{
	public interface IPassengerRepository : IRepository<Passenger>
	{
		void Update(Passenger passenger);
	}
}
