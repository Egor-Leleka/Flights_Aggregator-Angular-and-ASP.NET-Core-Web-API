
namespace FlightsAggregator.Models.Models.Rm
{
	public class FlightRm
	{
		public Guid Id { get; set; }
		public string Airline { get; set; }
		public string Price { get; set; }
		public TimePlaceRm Departure { get; set; }
		public TimePlaceRm Arrival { get; set; }
		public int RemainingNumberOfSeats { get; set; }
	}
}
