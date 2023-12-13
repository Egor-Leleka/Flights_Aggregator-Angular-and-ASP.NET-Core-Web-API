using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.Models.Domain.ApplicationDbContext;
using FlightsAggregator.Models.Models.Dto;
using FlightsAggregator.Models.Models.Rm;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAggregator.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PassengerController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public PassengerController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public IActionResult Register(NewPassengerDto dto)
		{
			_unitOfWork.Passenger.Add(new Passenger
			{
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Gender = dto.Gender
			});

			_unitOfWork.Save();

			return CreatedAtRoute("FindPassenger", new { email = dto.Email }, dto);
		}

		[HttpGet("{email}", Name = "FindPassenger")]
		public ActionResult<PassengerRm> Find(string email)
		{
			var passenger = _unitOfWork.Passenger.Get(p => p.Email == email);

			if (passenger == null)
				return NotFound();

			var rm = new PassengerRm
			{
				Email = passenger.Email,
				FirstName = passenger.FirstName,
				LastName = passenger.LastName,
				Gender = passenger.Gender
			};

			return Ok(rm);
		}
	}
}
