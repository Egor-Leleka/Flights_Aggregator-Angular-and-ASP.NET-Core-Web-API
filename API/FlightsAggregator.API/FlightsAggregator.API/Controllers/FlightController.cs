using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.Models.Domain.ApplicationDbContext;
using FlightsAggregator.Models.Models.Dto;
using FlightsAggregator.Models.Models.Rm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightsAggregator.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlightController : ControllerBase
	{
		private readonly ILogger<FlightController> _logger;

		private readonly IUnitOfWork _unitOfWork;


		public FlightController(
			ILogger<FlightController> logger,
			IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
		public IEnumerable<FlightRm> Search([FromQuery] FlightSearchParameters @params)
		{

			_logger.LogInformation("Searching for a flight for: {Destination}", @params.Destination);

			IQueryable<Flight> flights = _unitOfWork.Flight.GetAll().AsQueryable();

			if (!string.IsNullOrWhiteSpace(@params.Destination))
				flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));

			if (!string.IsNullOrWhiteSpace(@params.From))
				flights = flights.Where(f => f.Departure.Place.Contains(@params.From));

			if (@params.FromDate != null)
				flights = flights.Where(f => f.Departure.Time >= @params.FromDate.Value.Date);

			if (@params.ToDate != null)
				flights = flights.Where(f => f.Departure.Time >= @params.ToDate.Value.Date.AddDays(1).AddTicks(-1));

			if (@params.NumberOfPassengers != 0 && @params.NumberOfPassengers != null)
				flights = flights.Where(f => f.RemainingNumberOfSeats >= @params.NumberOfPassengers);
			else
				flights = flights.Where(f => f.RemainingNumberOfSeats >= 1);
			var flightRmList = flights
				.Select(flight => new FlightRm
				{
					Id = flight.Id,
					Airline = flight.Airline,
					Price = flight.Price,
					Departure = new TimePlaceRm { Place = flight.Departure.Place.ToString(), Time = flight.Departure.Time },
					Arrival = new TimePlaceRm { Place = flight.Arrival.Place.ToString(), Time = flight.Arrival.Time },
					RemainingNumberOfSeats = flight.RemainingNumberOfSeats
				});

			return flightRmList;
		}


		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet("{id}", Name = "FindFlight")]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[ProducesResponseType(typeof(FlightRm), 200)]
		public ActionResult<FlightRm> Find(Guid id)
		{
			var flight = _unitOfWork.Flight.Get(f => f.Id == id);

			if (flight == null)
				return NotFound();
			var readModel = new FlightRm
			{
				Id = flight.Id,
				Airline = flight.Airline,
				Price = flight.Price,
				Departure = new TimePlaceRm { Place = flight.Departure.Place.ToString(), Time = flight.Departure.Time },
				Arrival = new TimePlaceRm { Place = flight.Arrival.Place.ToString(), Time = flight.Arrival.Time },
				RemainingNumberOfSeats = flight.RemainingNumberOfSeats
			};

			return Ok(readModel);
		}

		[HttpPost]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public IActionResult Book(BookDto dto)
		{
			System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");

			var flight = _unitOfWork.Flight.Get(f => f.Id == dto.FlightId);

			if (flight == null)
				return NotFound();

			if (flight.RemainingNumberOfSeats < dto.NumberOfSeats)
				return Conflict(new { message = "Not enough seats." });

			var booking = new Booking()
			{
				FlightId = dto.FlightId,
				Flight = flight,
				PassengerEmail = dto.PassengerEmail,
				NumberOfSeats = dto.NumberOfSeats,
			};

			_unitOfWork.Booking.Add(booking);

			flight.RemainingNumberOfSeats -= dto.NumberOfSeats;

			_unitOfWork.Flight.Update(flight);

			try
			{
				_unitOfWork.Save();
			}
			catch (DbUpdateConcurrencyException e)
			{
				return Conflict(new { message = "An error occurred while booking. Please try again." });
			}

			return CreatedAtRoute("FindFlight", new { id = dto.FlightId }, dto);
		}
	}
}
