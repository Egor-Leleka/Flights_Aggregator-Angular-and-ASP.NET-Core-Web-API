using System.ComponentModel.DataAnnotations;

namespace FlightsAggregator.Models.Domain.ApplicationDbContext
{
    public class Flight
    {
		[Key]
        public Guid Id { get; set; }
        [Required]
        public string Airline { get; set; }
		[Required]
		public string Price { get; set; }
		[Required]
		public TimePlace Departure { get; set; }
		[Required]
		public TimePlace Arrival { get; set; }
		[Required]
		public int RemainingNumberOfSeats { get; set; }
	}
}
