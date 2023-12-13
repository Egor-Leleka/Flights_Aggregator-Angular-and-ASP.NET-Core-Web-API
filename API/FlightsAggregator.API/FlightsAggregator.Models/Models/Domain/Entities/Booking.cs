using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightsAggregator.Models.Domain.ApplicationDbContext
{
    public class Booking
    {
		[Key]
        public int Id { get; set; }

        public Guid FlightId { get; set; }
		
		[ForeignKey("FlightId")]
		public Flight Flight { get; set; }

		public string PassengerEmail { get; set; }

		public byte NumberOfSeats { get; set; }
        
	}
}
