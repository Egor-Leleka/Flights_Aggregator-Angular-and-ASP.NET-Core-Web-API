using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightsAggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arrival_Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemainingNumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSeats = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Airline", "Price", "RemainingNumberOfSeats", "Arrival_Place", "Arrival_Time", "Departure_Place", "Departure_Time" },
                values: new object[,]
                {
                    { new Guid("572276eb-e09a-47be-a01c-a2bf88cd5898"), "American Airlines", "3318", 205, "Istanbul", new DateTime(2023, 12, 19, 18, 15, 42, 277, DateTimeKind.Local).AddTicks(1165), "Los Angeles", new DateTime(2023, 12, 19, 12, 15, 42, 277, DateTimeKind.Local).AddTicks(1040) },
                    { new Guid("6ce7df58-6735-41a8-aedd-2bc2d697ec8a"), "British Airways", "3276", 195, "Vizzola-Ticino", new DateTime(2023, 12, 19, 22, 15, 42, 277, DateTimeKind.Local).AddTicks(1170), "London", new DateTime(2023, 12, 19, 12, 15, 42, 277, DateTimeKind.Local).AddTicks(1104) },
                    { new Guid("a3082f01-a9fb-43b3-951e-caef3f4ced4f"), "Deutsche BA", "3376", 463, "Schiphol", new DateTime(2023, 12, 19, 20, 15, 42, 277, DateTimeKind.Local).AddTicks(1169), "Munchen", new DateTime(2023, 12, 19, 12, 15, 42, 277, DateTimeKind.Local).AddTicks(1101) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
