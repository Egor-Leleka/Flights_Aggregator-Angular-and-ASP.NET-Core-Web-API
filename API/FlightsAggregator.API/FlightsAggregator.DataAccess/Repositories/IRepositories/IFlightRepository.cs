using FlightsAggregator.Models.Domain.ApplicationDbContext;

namespace FlightsAggregator.DataAccess.Repositories.IRepositories
{
	public interface IFlightRepository: IRepository<Flight>
	{
		void Update(Flight flight);
	}
}
