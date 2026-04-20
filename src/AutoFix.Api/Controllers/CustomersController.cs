using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
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
        var result= await _mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);

            }
            return Ok(result.Value);
        }
    }
}
