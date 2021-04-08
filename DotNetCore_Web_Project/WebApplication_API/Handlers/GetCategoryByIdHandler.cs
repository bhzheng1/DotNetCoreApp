using AutoMapper;
using MediatR;
using ModelClassLibrary;
using ModelClassLibrary.ResponseModel;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_API.Queries;
using WebApplication_API.Repositories;

namespace WebApplication_API.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CategoryModel>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            return new ApiResponse<CategoryModel> {
                Data = _mapper.Map<CategoryModel>(result)};
        }
    }
}
