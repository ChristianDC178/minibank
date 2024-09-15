using FluentValidation;
using MiniBank.CustomersSrv.Application.Dtos.Requests;

namespace MiniBank.CustomersSrv.Application.Dtos.Validators;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(20)
            .WithMessage((req) =>
            {
                return string.Empty;
            });

        //RuleFor(x => x.Forename).NotEmpty().WithMessage("Please specify a first name");
        //RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
        //RuleFor(x => x.Address).Length(20, 250);
        //RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
    }
}
    
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
                return string.Empty;
            });
      
    }
}
