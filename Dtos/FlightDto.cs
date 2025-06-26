using System;

namespace AmadeusFlightApi.Dtos
{
    public class FlightDto
    {
        public Guid Id { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public decimal Price { get; set; }
        public string? DepartureAirportCity { get; set; }
        public string? ArrivalAirportCity { get; set; }
    }

    public class CreateFlightDto
    {
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateFlightDto
    {
        public DateTime DepartureDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public decimal Price { get; set; }
    }
}
