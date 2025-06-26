using FluentValidation;
using System;

namespace AmadeusFlightApi.Dtos
{
    public class CreateFlightDtoValidator : AbstractValidator<CreateFlightDto>
    {
        public CreateFlightDtoValidator()
        {
            RuleFor(x => x.DepartureAirportId).NotEmpty();
            RuleFor(x => x.ArrivalAirportId).NotEmpty().NotEqual(x => x.DepartureAirportId).WithMessage("Arrival airport must be different from departure airport.");
            RuleFor(x => x.DepartureDateTime).GreaterThan(DateTime.UtcNow.AddDays(-1)).WithMessage("Departure date must be in the future.");
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }

    public class UpdateFlightDtoValidator : AbstractValidator<UpdateFlightDto>
    {
        public UpdateFlightDtoValidator()
        {
            RuleFor(x => x.DepartureDateTime).GreaterThan(DateTime.UtcNow.AddDays(-1));
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
