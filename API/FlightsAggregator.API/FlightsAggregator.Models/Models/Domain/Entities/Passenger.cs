using System.ComponentModel.DataAnnotations;

namespace FlightsAggregator.Models.Domain.ApplicationDbContext
{
	public class Passenger
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool Gender { get; set; }
	}
}
