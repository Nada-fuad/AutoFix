using AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask;
using AutoFix.Application.Features.RepairTasks.Commands.RemoveRepairTask;
using AutoFix.Application.Features.RepairTasks.Commands.UpdateRepairTask;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Application.Features.RepairTasks.Queries.GetRepairTaskById;
using AutoFix.Application.Features.RepairTasks.Queries.GetRepairTasks;
using AutoFix.Contracts.Requests.RepairTasks;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks.Enums;
using AutoFix.Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Linq;
namespace AutoFix.Api.Controllers
{
    [Route("api/repair-tasks")]
    //[Authorize]
    public class RepairTasksController(ISender sender) : ApiController
    {



        [HttpGet]
        [ProducesResponseType(typeof(List<RepairTaskDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves all repair tasks.")]
        [EndpointDescription("Returns a list of all repair tasks available in the system.")]
        [EndpointName("GetRepairTasks")]
       
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetRepairTasksQuery(), ct);
            return result.Match(
              response => Ok(response),
              Problem);
        }





        [HttpGet("{repairTaskId:guid}", Name = nameof(GetById))]
        [ProducesResponseType(typeof(RepairTaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves a repair task by ID.")]
        [EndpointDescription("Returns detailed information for the specified repair task if it exists.")]
        [EndpointName("GetRepairTaskById")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetById(Guid repairTaskId,CancellationToken ct)
        {
            var result = await sender.Send(new GetRepairTaskByIdQuery(repairTaskId), ct);

            return result.Match(
                response => Ok(response),
                Problem);
        }



        [HttpPost]
        [ProducesResponseType(typeof(RepairTaskDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new repair task.")]
        [EndpointDescription("Creates a repair task and optionally includes parts.")]
        [EndpointName("CreateRepairTask")]
        public async Task<IActionResult> Create([FromBody] CreateRepairTaskRequest request,CancellationToken ct)
        {
            var parts = request.Parts
       .ConvertAll(p => new CreateRepairTaskPartCommand(p.Name, p.Cost, p.Quantity))
;
            var duration = request.EstimatedDurationInMins is not null ?(RepairDurationInMinutes)request.EstimatedDurationInMins.Value: RepairDurationInMinutes.Min30;
            var command = new CreateRepairTaskCommand(
                request.Name,
                request.LaborCost,
                duration,
                parts);

            var result = await sender.Send(command, ct);
            return result.Match(
            response => CreatedAtAction(nameof(GetById), new { repairTaskId = response.RepairTaskId }, response),
            Problem);

        }






        [HttpPut("{repairTaskId:guid}")]
        [ProducesResponseType(typeof(RepairTaskDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing repair task.")]
        [EndpointDescription("Updates a repair task and its associated parts.")]
        [EndpointName("UpdateRepairTask")]

        public async Task<IActionResult> Update(Guid repairTaskId, [FromBody] UpdateRepairTaskRequest request, CancellationToken ct)
        {
            var parts = request.Parts
           .ConvertAll(p => new UpdateRepairTaskPartCommand(p.PartId, p.Name, p.Cost, p.Quantity))
;

            var command = new UpdateRepairTaskCommand(
                repairTaskId,
                request.Name,
                request.LaborCost,
                (RepairDurationInMinutes)request.EstimatedDurationInMins,
                parts);

            var result = await sender.Send(command, ct);

            return result.Match(
                response => Ok(response),
                Problem);

        }




        [HttpDelete("{repairTaskId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Removes a repair task.")]
        [EndpointDescription("Deletes the specified repair task from the system.")]
        [EndpointName("RemoveRepairTask")]

        public async Task<IActionResult> Delete(Guid repairTaskId,CancellationToken ct)
        {
            var result = await sender.Send(new RemoveRepairTaskCommand(repairTaskId), ct);

            return result.Match(
          _ => NoContent(),
          Problem);

        }


    }
}
