using System.Reflection;
using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
using AutoFix.Application.Features.Customers.Commands.DeleteCustomer;
using AutoFix.Application.Features.Customers.Commands.UpdateCustomer;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Application.Features.Customers.Queries.GetCustomerById;
using AutoFix.Application.Features.Customers.Queries.GetCustomers;
using AutoFix.Contracts.Requests.Customers;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace AutoFix.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    //[Authorize]

    public sealed class CustomersController(ISender sender) : ApiController
    {




        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        [EndpointSummary("Retrieves a list of customers.")]
        [EndpointDescription("Returns all customers associated with the current user.")]
        [EndpointName("GetCustomers")]
        [ProducesDefaultResponseType]
           [OutputCache(Duration = 60)]

        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetCustomersQuery(),ct);

            return result.Match(response => Ok(response), Problem);
        }







        [HttpGet("{customerId:guid}",Name = "GetCustomerById")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves a customer by ID.")]
        [EndpointDescription("Returns detailed information about the specified customer if found.")]
        [EndpointName("GetCustomerById")]
        [OutputCache(Duration = 60)]


        public async Task<IActionResult> GetCustomerById(Guid customerId,CancellationToken ct)
        {
            var result = await sender.Send(new GetCustomerByIdQuery(customerId));
            return result.Match(
                        response => Ok(response),
                        Problem);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new customer.")]
        [EndpointDescription("Adds a new customer to the system.")]
        [EndpointName("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody]CreateCustomerRequest request,CancellationToken ct)
        {
            var vehicles = request.Vehicles
           .ConvertAll(v => new CreateVehicleCommand(v.Make, v.Model, v.Year, v.LicensePlate));


            var result = await sender.Send(
            new CreateCustomerCommand(
            request.Name,
           
            request.Email,
             request.PhoneNumber,
            vehicles),
            ct);


            return result.Match(
            response => CreatedAtRoute(
                routeName: "GetCustomerById",
                routeValues: new { version = "1.0", customerId = response.CustomerId },
                value: response),
            Problem);


        }

        [HttpPut("{customerId:guid}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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
           
            request.Email,
             request.PhoneNumber,
            vehicles);
            var result = await sender.Send(command,ct);


            return result.Match(
            response => Ok(response),
            Problem);

        }


        [HttpDelete("{customerId:guid}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        [EndpointSummary("Removes a customer.")]
        [EndpointDescription("Deletes the specified customer from the system.")]
        [EndpointName("RemoveCustomer")]
        public async Task<IActionResult> Delete(Guid customerId,CancellationToken ct)
        {

            var result = await sender.Send(new RemoveCustomerCommand(customerId),ct);

            return result.Match(
          _ => NoContent(),
          Problem);

        }

        

       
    }
}
