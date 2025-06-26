using FluentValidation;

namespace AmadeusFlightApi.Dtos
{
    public class CreateAirportDtoValidator : AbstractValidator<CreateAirportDto>
    {
        public CreateAirportDtoValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        }
    }

    public class UpdateAirportDtoValidator : AbstractValidator<UpdateAirportDto>
    {
        public UpdateAirportDtoValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        }
    }
}
