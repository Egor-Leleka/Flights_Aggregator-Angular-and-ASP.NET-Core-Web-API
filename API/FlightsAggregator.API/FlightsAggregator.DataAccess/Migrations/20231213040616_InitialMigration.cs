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
                    { new Guid("3ed06715-0de2-45b4-9981-be85678298ba"), "British Airways", "4248", 561, "Vizzola-Ticino", new DateTime(2023, 12, 14, 3, 6, 16, 706, DateTimeKind.Local).AddTicks(8899), "London", new DateTime(2023, 12, 13, 17, 6, 16, 706, DateTimeKind.Local).AddTicks(8763) },
                    { new Guid("ad8a3d85-a5e3-457d-b9ac-b593a2d8ee04"), "American Airlines", "895", 283, "Istanbul", new DateTime(2023, 12, 13, 23, 6, 16, 706, DateTimeKind.Local).AddTicks(8889), "Los Angeles", new DateTime(2023, 12, 13, 17, 6, 16, 706, DateTimeKind.Local).AddTicks(8687) },
                    { new Guid("de443a10-0590-45fb-a979-236e02f71da4"), "Deutsche BA", "3461", 454, "Schiphol", new DateTime(2023, 12, 14, 1, 6, 16, 706, DateTimeKind.Local).AddTicks(8896), "Munchen", new DateTime(2023, 12, 13, 17, 6, 16, 706, DateTimeKind.Local).AddTicks(8759) }
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
