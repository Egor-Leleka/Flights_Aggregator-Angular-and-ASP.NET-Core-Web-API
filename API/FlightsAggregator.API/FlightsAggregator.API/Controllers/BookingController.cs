using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.Models.Models.Dto;
using FlightsAggregator.Models.Models.Rm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightsAggregator.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public BookingController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet("{email}")]
		[ProducesResponseType(500)]
		[ProducesResponseType(400)]
		[ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
		public ActionResult<IEnumerable<BookingRm>> List(string email)
		{
			var bookings = _unitOfWork.Booking.GetAll()
					   .Include(b => b.Flight)
					   .Where(b => b.PassengerEmail.ToLower() == email.ToLower()).ToList();

			if (!bookings.Any())
				return NoContent();

			var bookingRms = bookings.Select(b => new BookingRm
			{
				FlightId = b.FlightId,
				Airline = b.Flight.Airline,
				Price = b.Flight.Price,
				Departure = new TimePlaceRm { Place = b.Flight.Departure.Place, Time = b.Flight.Departure.Time},
				Arrival = new TimePlaceRm { Place = b.Flight.Arrival.Place, Time = b.Flight.Arrival.Time },
				NumberOfBookedSeats = b.NumberOfSeats,
				PassengerEmail = b.PassengerEmail
			});

			return Ok(bookingRms);
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(500)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult Cancel(BookDto dto)
		{

			var flight = _unitOfWork.Flight.Get(f => f.Id == dto.FlightId);

			var booking = _unitOfWork.Booking.Get(b => b.NumberOfSeats == dto.NumberOfSeats
		   && b.PassengerEmail.ToLower() == dto.PassengerEmail.ToLower());

			if (booking == null)
				return NotFound();

			_unitOfWork.Booking.Remove(booking);

			flight.RemainingNumberOfSeats += booking.NumberOfSeats;

			_unitOfWork.Flight.Update(flight);
			_unitOfWork.Save();

			return NoContent();
		}
	}
}

