using FluentValidation;
using WebApplication_API.Commands;

namespace WebApplication_API.Validators
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.address1).NotEmpty().WithMessage("address1 is empty");
        }
    }
}
