using FlightsAggregator.DataAccess.Data;
using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.Models.Domain.ApplicationDbContext;

namespace FlightsAggregator.DataAccess.Repositories
{
	public class BookingRepository : Repository<Booking>, IBookingRepository
	{
		private ApplicationDbContext _repository;

		public BookingRepository(ApplicationDbContext repository) : base(repository)
		{
			_repository = repository;
		}

		public void Update(Booking booking)
		{
			_repository.Update(booking);
		}
	}
}
