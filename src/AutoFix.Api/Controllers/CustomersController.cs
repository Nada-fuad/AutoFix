using System.Reflection;
using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
using AutoFix.Application.Features.Customers.Commands.DeleteCustomer;
using AutoFix.Application.Features.Customers.Commands.UpdateCustomer;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Application.Features.Customers.Queries.GetCustomerById;
using AutoFix.Application.Features.Customers.Queries.GetCustomers;
using AutoFix.Contracts.Requests.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AutoFix.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) { _mediator = mediator; }


        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDto>),StatusCodes.Status200OK)]
        [EndpointSummary("Retrieves a list of customers.")]
        [EndpointDescription("Returns all customers associated with the current user.")]
        [EndpointName("GetCustomers")]
        [ProducesDefaultResponseType]


        public async Task<IActionResult> GetCustomers(CancellationToken ct)
        {
            var customers = await _mediator.Send(new GetCustomersQuery(),ct);
           
            return Ok(customers);
        }

        [HttpGet("{customerId:guid}",Name = "GetCustomerById")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [EndpointSummary("Retrieves a customer by ID.")]
        [EndpointDescription("Returns detailed information about the specified customer if found.")]
        [EndpointName("GetCustomerById")]

        public async Task<IActionResult> GetCustomerById(Guid customerId,CancellationToken ct)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(customerId));

            if (customer.IsError)
            {
                return NotFound();
            }
            return Ok(customer.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto),StatusCodes.Status201Created)]
        [Consumes("application/json")]
        [EndpointSummary("Creates a new customer.")]
        [EndpointDescription("Adds a new customer to the system.")]
        [EndpointName("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request,CancellationToken ct)
        {
            var vehicles = request.Vehicles
           .ConvertAll(v => new CreateVehicleCommand(v.Make, v.Model, v.Year, v.LicensePlate));
            var result = await _mediator.Send(
                      new CreateCustomerCommand(
                      request.Name,
                      request.PhoneNumber,
                      request.Email,
                      vehicles),
                      ct);




            if (result.IsError)
            {
                return BadRequest(result.Errors);

            }
            return Ok(result.Value);
        }

        [HttpPut("{customerId:guid}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status204NoContent)]
        [EndpointSummary("Updates an existing customer.")]
        [EndpointDescription("Updates a customer and its associated vehicle.")]
        [EndpointName("UpdateCustomer")]
        
        public async Task<IActionResult> Update(Guid customerId, [FromBody] UpdateCustomerRequest request, CancellationToken ct)
        {
            var vehicles = request.Vehicles
          .ConvertAll(v => new UpdateVehicleCommand(v.VehicleId, v.Make, v.Model, v.Year, v.LicensePlate));
            var command = new UpdateCustomerCommand(
            customerId,
            request.Name,
            request.PhoneNumber,
            request.Email,
            vehicles);
            var result = await _mediator.Send(command,ct);
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }


        [HttpDelete("{customerId:guid}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        
        [EndpointSummary("Removes a customer.")]
        [EndpointDescription("Deletes the specified customer from the system.")]
        [EndpointName("RemoveCustomer")]
        public async Task<IActionResult> Delete(Guid customerId,CancellationToken ct)
        {

            var result = await _mediator.Send(new RemoveCustomerCommand(customerId),ct);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }

        

       
    }
}
