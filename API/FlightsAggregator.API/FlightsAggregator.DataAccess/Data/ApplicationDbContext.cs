using FlightsAggregator.Models.Domain.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlightsAggregator.DataAccess.Data
{
	public class ApplicationDbContext : DbContext
	{
		Random random = new Random();

		public DbSet<Flight> Flights { get; set; }
		public DbSet<Passenger> Passengers { get; set; }
		public DbSet<Booking> Bookings { get; set; }

		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options) : base(options) { }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

			// Make Departure and Arrival as a part of Flight table.
			modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
			modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);

			// Seed Flight without Departure and Arrival.
			var flightId1 = Guid.NewGuid();
			var flightId2 = Guid.NewGuid();
			var flightId3 = Guid.NewGuid();

			modelBuilder.Entity<Flight>().HasData(

				new Flight
				{
					Id = flightId1,
					Airline = "American Airlines",
					Price = random.Next(90, 5000).ToString(),
					RemainingNumberOfSeats = random.Next(50, 800)
				},
				new Flight
				{
					Id = flightId2,
					Airline = "Deutsche BA",
					Price = random.Next(90, 5000).ToString(),
					RemainingNumberOfSeats = random.Next(50, 800)
				},
				new Flight
				{
					Id = flightId3,
					Airline = "British Airways",
					Price = random.Next(90, 5000).ToString(),
					RemainingNumberOfSeats = random.Next(50, 800)
				});

			// Seed Departure and Arrival to Flight table separately.
			modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure).HasData(
				new { FlightId = flightId1, Place = "Los Angeles", Time = DateTime.Now },
				new { FlightId = flightId2, Place = "Munchen", Time = DateTime.Now },
				new { FlightId = flightId3, Place = "London", Time = DateTime.Now });

			modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival).HasData(
				new { FlightId = flightId1, Place = "Istanbul", Time = DateTime.Now.AddHours(6) },
				new { FlightId = flightId2, Place = "Schiphol", Time = DateTime.Now.AddHours(8) },
				new { FlightId = flightId3, Place = "Vizzola-Ticino", Time = DateTime.Now.AddHours(10) });
		}
	}
}
