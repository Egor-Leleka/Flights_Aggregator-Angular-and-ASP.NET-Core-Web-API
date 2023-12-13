using FlightsAggregator.DataAccess.Data;
using FlightsAggregator.DataAccess.Repositories.IRepositories;

namespace FlightsAggregator.DataAccess.Repositories
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly ApplicationDbContext _repository;
		public IBookingRepository Booking {  get; private set; }
		public IFlightRepository Flight { get; private set; }
		public IPassengerRepository Passenger { get; private set; }

        public UnitOfWork(ApplicationDbContext repository)
        {
			_repository = repository;
			Booking = new BookingRepository(_repository);
			Flight = new FlightRepository(_repository);
			Passenger = new PassengerRepository(_repository);
        }

		public void Save()
		{
			_repository.SaveChanges();
		}
	}
}
