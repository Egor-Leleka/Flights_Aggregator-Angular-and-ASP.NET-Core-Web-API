using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsAggregator.DataAccess.Repositories.IRepositories
{
	public interface IUnitOfWork
	{
		IBookingRepository Booking { get; }
		IFlightRepository Flight { get; }
		IPassengerRepository Passenger { get; }
		void Save();
	}
}
