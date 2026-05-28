using AutoFix.Application.Common.Models;
using AutoFix.Application.Features.WorkOrders.Commands.AssignLabor;
using AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder;
using AutoFix.Application.Features.WorkOrders.Commands.RecolateWorkOrder;
using AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrder;
using AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrderRepairTasks;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Application.Features.WorkOrders.Queries.GetWorkOrderById;
using AutoFix.Application.Features.WorkOrders.Queries.GetWorkOrders;
using AutoFix.Contracts.Requests.WorkOrders;
using AutoFix.Domain.WorkOrders;
using AutoFix.Domain.WorkOrders.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers
{
    [Route("api/workorders")]
    [Authorize]
    public class WorkOrdersController(ISender sender) :ApiController
    {

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<WorkOrderListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves a paginated list of work orders.")]
        [EndpointDescription("Supports filtering by date range, status, vehicle, labor, spot, and searching by term. Pagination and sorting are supported.")]
        [EndpointName("GetWorkOrders")]
        public async Task<IActionResult> Get([FromQuery] WorkOrderFilterRequest filters, [FromQuery] PageRequest pageRequest, CancellationToken ct)
        {
            if (pageRequest.Page <= 0)
            {
                return BadRequest("Page must be greater than 0");
            }

            if (pageRequest.PageSize <= 0 || pageRequest.PageSize > 100)
            {
                return BadRequest("PageSize must be between 1 and 100");
            }

            var query = new GetWorkOrdersQuery(
                pageRequest.Page,
                pageRequest.PageSize,
                filters.SearchTerm,
                filters.SortColumn,
                filters.SortDirection,
                filters.State is not null ? (WorkOrderState)(int)filters.State : null,
                filters.VehicleId,
                filters.LaborId,
                filters.StartDateFrom,
                filters.StartDateTo,
                filters.EndDateFrom,
                filters.EndDateTo,
                filters.Spot is not null ? (Spot)(int)filters.Spot : null);

            var result = await sender.Send(query, ct);

            return result.Match(
                response => Ok(response),
                Problem);
        }




        [HttpGet("{workOrderId:guid}", Name = "GetWorkOrderById")]
        [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves a work order by its ID.")]
        [EndpointDescription("Returns detailed information about the specified work order if it exists.")]
        [EndpointName("GetWorkOrderById")]
        public async Task<IActionResult> GetById(Guid workOrderId, CancellationToken ct)
        {
            var result = await sender.Send(new GetWorkOrderByIdQuery(workOrderId), ct);

            return result.Match(
              response => Ok(response),
              Problem);
        }








        [HttpPost]
        [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new work order.")]
        [EndpointDescription("Creates a new work order for a vehicle, specifying labor, tasks, and other required information.")]
        [EndpointName("CreateWorkOrder")]
        public async Task<IActionResult> Create([FromBody] CreateWorkOrderRequest request, CancellationToken ct)
        {
            var result = await sender.Send(
                new CreateWorkOrderCommand(
                (Spot)(int)request.Spot,
                request.VehicleId,
                request.StartAtUtc,
                request.RepairTaskIds,
                request.LaborId),
                ct);

            return result.Match(
                response => CreatedAtRoute(
                    routeName: "GetWorkOrderById",
                    routeValues: new { version = "1.0", workOrderId = response.WorkOrderId },
                    value: response),
                Problem);
        }





        
        [HttpDelete("{workOrderId:guid}")]

        public async Task<IActionResult> Delete(Guid workOrderId)
        {

            var command= new DeleteWorkOrderCommand(workOrderId);

            var result= await sender.Send(command);
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

            var result = await sender.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }


        [HttpPut("{workorderId:guid}/repairtasks")]

        public async Task<IActionResult> UpdateRepairTasks(Guid workOrderId,[FromBody]Guid[] repairTaskIds)
        {
            var command = new UpdateWorkOrderRepairTasksCommand(workOrderId, repairTaskIds);

            var result = await sender.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }


        [HttpPut("{workOrderId:Guid}/labor/{laborId:guid}")]

        public async Task<IActionResult> AssignLabor(Guid workOrderId ,Guid laborId
            )
        {
            var command = new AssignLaborCommand(workOrderId,laborId);

            var result = await sender.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }

        [HttpPut("{workOrderId:guid}/relocation")]


        public async Task<IActionResult> RecolateWorkOrder(Guid workOrderId,RelocateWorkOrderRequest request ,   CancellationToken ct)
        {

            var command = new RelocateWorkOrderCommand(workOrderId,request.NewStartAtUtc,(Spot)(int)request.NewSpot);



            var result= await sender.Send(command,ct);  

            if (result.IsError) {

                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
    }
    }
