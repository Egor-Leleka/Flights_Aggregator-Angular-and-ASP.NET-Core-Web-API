using System.ComponentModel.DataAnnotations;

namespace FlightsAggregator.Models.Models.Dto
{
	public class NewPassengerDto
	{
		[Required]
		[EmailAddress]
		[StringLength(100, MinimumLength = 3)]
		public string Email { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(35)]
		public string FirstName { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(35)]
		public string LastName { get; set; }

		[Required]
		public bool Gender { get; set; }
	}
}
