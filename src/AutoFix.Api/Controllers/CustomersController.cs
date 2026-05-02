using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
using AutoFix.Application.Features.Customers.Commands.DeleteCustomer;
using AutoFix.Application.Features.Customers.Commands.UpdateCustomer;
using AutoFix.Application.Features.Customers.Queries.GetCustomerById;
using AutoFix.Application.Features.Customers.Queries.GetCustomers;
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
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);

            }
            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
        {



            var updatedCommand = command with { CustomerId = id };
            var result = await _mediator.Send(updatedCommand);
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
