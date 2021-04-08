using MediatR;
using ModelClassLibrary;
using ModelClassLibrary.ResponseModel;
using WebApplication_API.Validators;

namespace WebApplication_API.Commands
{
    public class CreateCategoryCommand: IRequest<ValidateableResponse<int>>
    {
        public CreateCategoryDto CreateCategoryDto { get; set; }
    }
}
