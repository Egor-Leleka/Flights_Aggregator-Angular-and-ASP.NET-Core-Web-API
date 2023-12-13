using System.ComponentModel;

namespace FlightsAggregator.Models.Models.Dto
{
	public class FlightSearchParameters
	{
		public DateTime? FromDate { get; set; }

		public DateTime? ToDate { get; set; }

		public string? From {  get; set; }

		public string? Destination { get; set; }

		public int? NumberOfPassengers { get; set; }
	}
}
