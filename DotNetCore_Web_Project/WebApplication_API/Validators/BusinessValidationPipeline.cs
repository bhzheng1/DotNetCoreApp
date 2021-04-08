using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ModelClassLibrary.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication_API.Validators
{
    public class BusinessValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TRequest> _logger;

        public BusinessValidationPipeline(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Any())
            {
                _logger.LogError("bad request");

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(ValidateableResponse<>).MakeGenericType(resultType);
                    var invalidResponse =
                        Activator.CreateInstance(invalidResponseType, null, failures.Select(s => s.ErrorMessage).ToList()) as TResponse;

                    return invalidResponse;
                }
            }

            var response = await next();

            return response;
        }
    }
}
