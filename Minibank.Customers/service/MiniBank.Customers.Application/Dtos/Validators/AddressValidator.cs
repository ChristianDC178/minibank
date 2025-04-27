using FluentValidation;
using MiniBank.CustomersSrv.Application.Dtos.Requests;

namespace MiniBank.CustomersSrv.Application.Dtos.Validators;

public class AddressValidator : AbstractValidator<CreateCustomerAddressRequest>
{
    public AddressValidator()
    {
        RuleFor(x => x.StreetName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(20)
            .WithMessage((req) =>
            {
                return "Street name is required";
            });

        RuleFor(x => x.City)
          .NotEmpty()
          .NotNull()
          .MinimumLength(3)
          .MaximumLength(20)
          .WithMessage((req) =>
          {
              return "City is required";
          });


        RuleFor(x => x.State)
          .NotEmpty()
          .NotNull()
          .MinimumLength(3)
          .MaximumLength(20)
          .WithMessage((req) =>
          {
              return "State is required";
          });


    }
}
