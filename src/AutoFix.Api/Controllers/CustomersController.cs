using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
using AutoFix.Application.Features.Customers.Commands.RemoveCustomer;
using AutoFix.Application.Features.Customers.Commands.UpdateCustomer;
using AutoFix.Application.Features.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AutoFix.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)=> _mediator=mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command ,CancellationToken ct)
        {
            var result= await _mediator.Send(command,ct);
            if (result.IsError)
            {
                return BadRequest(result.Errors);

            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value.CustomerId }, result.Value);
        }

        [HttpGet("{id:guid}")]
        public async  Task<IActionResult> GetById(Guid id,CancellationToken ct)


        {
            var query = new GetCustomerByIdQuery(id);
            var result=await _mediator.Send(query,ct);

            if (result.IsError) return NotFound(result.Errors);

            return Ok(result.Value);
        }



        [HttpDelete("{id:guid}")]

        public async Task<IActionResult> Delete(Guid id ,CancellationToken ct)
        {
            var query = new RemoveCustomerCommand(id);

            var result = await _mediator.Send(query, ct);

            if (result.IsError) { return BadRequest(result.Errors); }

            return NoContent();
        }


        [HttpPut("{id:guid}")]

        public async Task<IActionResult> Update(Guid id,UpdateCustomerCommand body,CancellationToken ct)
        {
            var command = new UpdateCustomerCommand(id, body.Name, body.Email, body.PhoneNumber);
            var result =await _mediator.Send(command, ct);

            if (result.IsError) return BadRequest(result.Errors);
            return NoContent();
        }

    }

    
}
