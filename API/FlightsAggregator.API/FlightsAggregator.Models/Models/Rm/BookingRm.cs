
using FlightsAggregator.Models.Domain.ApplicationDbContext;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightsAggregator.Models.Models.Rm
{
	public class BookingRm
	{
		public Guid FlightId { get; set; }
		public string Airline { get; set; }
		public string Price { get; set; }
		public TimePlaceRm Departure { get; set; }
		public TimePlaceRm Arrival { get; set; }
		public int NumberOfBookedSeats { get; set; }
		public string PassengerEmail { get; set; }
	}
}
