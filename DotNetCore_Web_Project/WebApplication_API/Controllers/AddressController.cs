using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using System.Threading.Tasks;
using WebApplication_API.Commands;
using WebApplication_API.Queries;
using WebApplication_API.Repositories;

//本例演示mdiator pattern
namespace WebApplication_API.Controllers
{
    public class AddressController : Controller
    {

        private IAddressRepository _addressRepository;
        private readonly IMediator _mediator;

        public AddressController(IAddressRepository addressRepository, IMediator mediator)
        {
            _addressRepository = addressRepository;
            _mediator = mediator;
        }
        [HttpGet("GetAll0")]
        public async Task<IActionResult> GetAllAddresses0() {
            var result = await _addressRepository.GetAddresses();
            return Ok(result);
        }

        [HttpGet("GetById0/{addressId}")]
        public async Task<IActionResult> GetAddress0(int id)
        {
            var result = await _addressRepository.GetAddress(id);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost("CreateAddress0")]
        public async Task<IActionResult> GetAddress0([FromBody]AddressModel address)
        {
            var result = await _addressRepository.CreateAddress(address);
            return CreatedAtAction("GetAddress0",new {id = address.AddressId},result);
        }

        //mediator
        [HttpGet("GetAll1")]
        public async Task<IActionResult> GetAllAddresses1()
        {
            var query = new GetAllAddressQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetById1/{addressId}")]
        public async Task<IActionResult> GetAddress1(int id)
        {
            var query = new GetAddressByIdQuery(id);
            var result = await _mediator.Send(query);
            return result == null ? (IActionResult)NotFound() : Ok(result);
        }

        [HttpPost("CreateAddress1")]
        public async Task<IActionResult> CreateAddress1([FromBody]AddressModel address)
        {
            var command = new CreateAddressCommand
            {
                address1 = address.Address1,
                address2 = address.Address2
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAddress1), new { id = result.AddressId }, result);
        }
    }
}