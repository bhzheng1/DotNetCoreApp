using MediatR;
using ModelClassLibrary;
using ModelClassLibrary.ResponseModel;

namespace WebApplication_API.Queries
{
    public class GetCategoryByIdQuery : IRequest<ApiResponse<CategoryModel>>
    {
        public int Id { get; set; }
        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
