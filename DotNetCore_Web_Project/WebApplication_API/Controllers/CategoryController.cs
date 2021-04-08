using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelClassLibrary;
using ModelClassLibrary.ResponseModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_API.Commands;
using WebApplication_API.Queries;

namespace WebApplication_API.Controllers
{
    [ApiVersion("v1_0")]
    [Route("api/[controller]")]
    public class CategoryController : BaseApiController
    {
        public CategoryController(ILogger<CategoryController> logger, IMediator mediator):base(logger,mediator)
        {
        }

        [HttpGet("GetCategoryById/{addressId}")]
        public async Task<IActionResult> GetCategoryById(int id) {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            return OkResponse(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var result = await _mediator.Send(new CreateCategoryCommand { CreateCategoryDto = dto });
            return result.IsValidResponse ? CreatedAtRoute(routeValues: new
            {
                controller = "Category",
                action = nameof(CategoryController.GetCategoryById),
                id = result.Data,
                version = HttpContext.GetRequestedApiVersion().ToString()
            }, result.Data)
                : BadRequestResponse(Enumerable.Empty<string>(), operationOutcome: new OperationOutcome
                {
                    OpResult = OpResult.Fail,
                    IsError = false,
                    IsValidationFail = true,
                    Errors = result.Errors
                });
        }
    }
}
