using FlightsAggregator.Models.Domain.ApplicationDbContext;

namespace FlightsAggregator.DataAccess.Repositories.IRepositories
{
	public interface IBookingRepository : IRepository<Booking>
	{
		void Update(Booking booking);
	}
}
