using FlightsAggregator.DataAccess.Data;
using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.Models.Domain.ApplicationDbContext;

namespace FlightsAggregator.DataAccess.Repositories
{
	public class PassengerRepository : Repository<Passenger>, IPassengerRepository
	{
		private ApplicationDbContext _repository;

		public PassengerRepository(ApplicationDbContext repository) : base(repository)
		{
			_repository = repository;
		}

		public void Update(Passenger passenger)
		{
			_repository.Update(passenger);
		}
	}
}
