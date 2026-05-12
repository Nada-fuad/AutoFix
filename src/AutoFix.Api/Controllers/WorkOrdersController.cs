using AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder;
using AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrder;
using AutoFix.Application.Features.WorkOrders.Queries.GetWorkOrderByIdQuery;
using AutoFix.Contracts.Requests.WorkOrders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkOrderRequest request,CancellationToken ct)
        {
            var command = new CreateWorkOrderCommand(request.VehicleId,request.StartAtUtc,request.RepairTaskIds);
            var result=await _mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }

        [HttpDelete("{workOrderId:guid}")]

        public async Task<IActionResult> Delete(Guid workOrderId)
        {

            var command= new DeleteWorkOrderCommand(workOrderId);

            var result= await _mediator.Send(command);
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }


        [HttpGet("{workOrderId:guid}")]

        public async Task<IActionResult> GetById(Guid workOrderId)
        {
            var command= new GetWorkOrderByIdQuery(workOrderId);

            var result = await _mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }
    }
}
