using FluentValidation;
using ModelClassLibrary;
namespace WebApplication_API.Validators
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.LastUpdate).NotEmpty();
        }
    }
}
