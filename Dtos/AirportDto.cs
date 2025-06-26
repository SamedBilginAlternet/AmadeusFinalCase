using System;

namespace AmadeusFlightApý.Dtos
{
    public class AirportDto
    {
        public Guid Id { get; set; }
        public string City { get; set; } = null!;
    }

    public class CreateAirportDto
    {
        public string City { get; set; } = null!;
    }

    public class UpdateAirportDto
    {
        public string City { get; set; } = null!;
    }
}
