using System.ComponentModel.DataAnnotations;

namespace FlightsAggregator.Models.Models.Dto
{
	public class BookDto
	{
		[Required]
		public Guid FlightId { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(100, MinimumLength = 3)]
		public string PassengerEmail { get; set; }

		[Required][Range(1, 254)]
		public byte NumberOfSeats { get; set; }
	}
}
