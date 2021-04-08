using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using ModelClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_API.Commands;
using WebApplication_API.Repositories;
using WebApplication_API.Validators;
using System.Linq;

//Validation
namespace WebApplication_API.Handlers
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, AddressModel>
    {
        private readonly IAddressRepository _addressRepository;
        public CreateAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<AddressModel> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            Validate1(request);
            Validate2(request);
            var address = new AddressModel
            {
                Address1 = request.address1,
                Address2 = request.address2
            };
            var result = await _addressRepository.CreateAddress(address);
            return result;
        }

        //simple validation
        private static void Validate1(CreateAddressCommand command)
        {
            var errors = new List<string>();
            if (command.address1 == null) {
                errors.Add("Address1 is empty!");
            }

            //logic...

            if (errors.Any())
            {
                var errorBuilder = new StringBuilder();
                errorBuilder.AppendLine("Invalid order, reason: ");
                foreach (var error in errors)
                {
                    errorBuilder.AppendLine(error);
                }

                throw new Exception(errorBuilder.ToString());
            }
        }

        //Validation using FluentValidation library
        private static void Validate2(CreateAddressCommand command)
        {
            var validator = new CreateAddressCommandValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid) {
                var errorBuilder = new StringBuilder();
                errorBuilder.AppendLine("Invalid order, reason: ");
                foreach (var error in validationResult.Errors)
                {
                    errorBuilder.AppendLine(error.ErrorMessage);
                }

                throw new Exception(errorBuilder.ToString());
            }            
        }
    }
}
