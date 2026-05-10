using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
using AutoFix.Application.Features.Customers.Commands.DeleteCustomer;
using AutoFix.Application.Features.Customers.Commands.UpdateCustomer;
using AutoFix.Application.Features.Customers.Queries.GetCustomerById;
using AutoFix.Application.Features.Customers.Queries.GetCustomers;
using AutoFix.Contracts.Requests.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) { _mediator = mediator; }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {

            var command = new CreateCustomerCommand(

                request.Name, request.Email, request.PhoneNumber,
                request.Vehicles.Select(v => new CreateVehicleCommand(v.Make, v.Model, v.Year, v.LicensePlate)).ToList());




            var result = await _mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);

            }
            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerRequest request)
        {


            var command = new UpdateCustomerCommand(id, request.Name, request.Email, request.PhoneNumber);
            var result = await _mediator.Send(command);
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }


        [HttpDelete("{id:guid}")]


        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new RemoveCustomerCommand(id);

            var result = await _mediator.Send(command);


            return Ok(result.Value);

        }

        [HttpGet]

        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());

            return Ok(customers);
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer=await _mediator.Send(new GetCustomerByIdQuery(id));

            if (customer.IsError)
            {
                return NotFound();
            }
            return Ok(customer.Value);
        }
    }
}
