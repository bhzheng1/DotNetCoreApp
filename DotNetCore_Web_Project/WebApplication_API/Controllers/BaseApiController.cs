using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelClassLibrary.ResponseModel;

namespace WebApplication_API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;

        public BaseApiController(ILogger logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult OkResponse<T>(T data, string message = null)
        {
            var apiResponse = new ApiResponse<T>
            {
                Data = data,
                Outcome = new OperationOutcome
                {
                    Message = message ?? string.Empty,
                    OpResult = OpResult.Success
                }
            };

            return Ok(apiResponse);
        }

        public IActionResult OkResponse<T>(T data, OperationOutcome operationOutcome)
        {
            var apiResponse = new ApiResponse<T>
            {
                Data = data,
                Outcome = operationOutcome
            };

            return Ok(apiResponse);
        }

        public IActionResult OkResponse<T>(ApiResponse<T> apiResponse)
        {
            return Ok(apiResponse);
        }

        public IActionResult BadRequestResponse<T>(T data, string errorMessage = null, IEnumerable<string> errors = null)
        {
            var apiResponse = new ApiResponse<T>
            {
                Data = data,
                Outcome = new OperationOutcome
                {
                    Errors = errors ?? Enumerable.Empty<string>(),
                    Message = errorMessage ?? string.Empty,
                    OpResult = OpResult.Fail
                }
            };

            return BadRequest(apiResponse);
        }
        public IActionResult BadRequestResponse<T>(T data, OperationOutcome operationOutcome)
        {
            var apiResponse = new ApiResponse<T>
            {
                Data = data,
                Outcome = operationOutcome
            };

            return BadRequest(apiResponse);
        }

        public IActionResult BadRequestResponse<T>(ApiResponse<T> apiResponse)
        {
            return BadRequest(apiResponse);
        }
    }
}
