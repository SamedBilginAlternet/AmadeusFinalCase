using System;

namespace AmadeusFlightApý.Models
{
    public class Flight
    {
        public Guid Id { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public decimal Price { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
    }
}