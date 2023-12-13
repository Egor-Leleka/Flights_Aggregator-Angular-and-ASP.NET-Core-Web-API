using FlightsAggregator.DataAccess.Data;
using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.Models.Domain.ApplicationDbContext;

namespace FlightsAggregator.DataAccess.Repositories
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        private ApplicationDbContext _repository;

        public FlightRepository(ApplicationDbContext repository) : base(repository)
        {
            _repository = repository;
        }

        public void Update(Flight flight)
        {
            _repository.Update(flight);
        }
    }
}
