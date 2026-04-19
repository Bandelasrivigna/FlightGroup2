using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group2Flight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightCode = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    To = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    CabinType = table.Column<string>(type: "TEXT", nullable: false),
                    Emission = table.Column<double>(type: "REAL", nullable: false),
                    AircraftType = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flight_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightSelection",
                columns: table => new
                {
                    FlightSelectionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSelection", x => x.FlightSelectionId);
                    table.ForeignKey(
                        name: "FK_FlightSelection_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airline",
                columns: new[] { "AirlineId", "ImageName", "Name" },
                values: new object[,]
                {
                    { 1, "emirates_airlines.png", "Emirates Airlines" },
                    { 2, "qatar_airways.png", "Qatar Airways" },
                    { 3, "singapore_airlines.png", "Singapore Airlines" },
                    { 4, "lufthansa.png", "Lufthansa" },
                    { 5, "air_france.png", "Air France" },
                    { 6, "british_airways.png", "British Airways" }
                });

            migrationBuilder.InsertData(
                table: "Flight",
                columns: new[] { "FlightId", "AircraftType", "AirlineId", "ArrivalTime", "CabinType", "Date", "DepartureTime", "Emission", "FlightCode", "From", "Price", "To" },
                values: new object[,]
                {
                    { 1, "Airbus A380", 1, new TimeSpan(0, 22, 15, 0, 0), "Economy", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 30, 0, 0), 850.29999999999995, "EK206", "Dubai", 2500, "Sydney" },
                    { 2, "Boeing 787 Dreamliner", 2, new TimeSpan(0, 13, 30, 0, 0), "Business", new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 45, 0, 0), 320.80000000000001, "QR123", "Doha", 890, "Paris" },
                    { 3, "Airbus A350", 3, new TimeSpan(0, 17, 45, 0, 0), "Economy Plus", new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 15, 0, 0), 280.39999999999998, "SQ345", "Singapore", 680, "Tokyo" },
                    { 4, "Airbus A340", 4, new TimeSpan(0, 1, 20, 0, 0), "Economy", new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 50, 0, 0), 410.5, "LH456", "Frankfurt", 540, "Mumbai" },
                    { 5, "Boeing 777-300ER", 5, new TimeSpan(0, 6, 45, 0, 0), "Business", new DateTime(2026, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 23, 15, 0, 0), 620.70000000000005, "AF789", "Paris", 1350, "Rio de Janeiro" },
                    { 6, "Airbus A380", 6, new TimeSpan(0, 7, 15, 0, 0), "Economy Plus", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 30, 0, 0), 710.20000000000005, "BA890", "London", 980, "Cape Town" },
                    { 7, "Boeing 777-200LR", 1, new TimeSpan(0, 10, 30, 0, 0), "Business", new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 45, 0, 0), 780.5, "EK567", "Dubai", 2100, "Miami" },
                    { 8, "Airbus A330", 2, new TimeSpan(0, 12, 45, 0, 0), "Economy", new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 20, 0, 0), 350.89999999999998, "QR234", "Doha", 470, "Bangkok" },
                    { 9, "Airbus A350-900", 3, new TimeSpan(0, 8, 20, 0, 0), "Business", new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 40, 0, 0), 490.19999999999999, "SQ789", "Singapore", 1120, "Melbourne" },
                    { 10, "Boeing 747-8", 1, new TimeSpan(0, 9, 30, 0, 0), "Economy Plus", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 10, 0, 0), 550.29999999999995, "LH567", "Munich", 780, "Shanghai" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineId",
                table: "Flight",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSelection_FlightId",
                table: "FlightSelection",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightSelection");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Airline");
        }
    }
}
