using Dtos;
using FluentValidation;

namespace AmadeusFlightApý.Validators
{
    public class CreateAirportDtoValidator : AbstractValidator<CreateAirportDto>
    {
        public CreateAirportDtoValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City max length is 100.");
        }
    }

    public class UpdateAirportDtoValidator : AbstractValidator<UpdateAirportDto>
    {
        public UpdateAirportDtoValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City max length is 100.");
        }
    }
}
