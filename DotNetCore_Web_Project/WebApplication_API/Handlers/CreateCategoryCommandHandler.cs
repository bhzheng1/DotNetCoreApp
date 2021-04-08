using MediatR;
using ModelClassLibrary.ResponseModel;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_API.Commands;
using WebApplication_API.Repositories;
using WebApplication_API.SakilaModels;

namespace WebApplication_API.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ValidateableResponse<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ValidateableResponse<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateCategoryDto;
            var newCategory = new Category
            {
                Name = dto.Name,
                LastUpdate = dto.LastUpdate
            };

            var result = await _categoryRepository.CreateCategory(newCategory);
            if (result)
            {
                var apiResponse = new ValidateableResponse<int>()
                {
                    Data = 1,
                    Outcome = new OperationOutcome
                    {
                        Message = string.Empty,
                        OpResult = OpResult.Success
                    }

                };
                return apiResponse;
            }

            return new ValidateableResponse<int>();
        }
    }
}
