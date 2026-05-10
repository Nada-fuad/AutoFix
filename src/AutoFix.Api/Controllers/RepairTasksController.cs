using AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask;
using AutoFix.Application.Features.RepairTasks.Commands.RemoveRepairTask;
using AutoFix.Application.Features.RepairTasks.Commands.UpdateRepairTask;
using AutoFix.Application.Features.RepairTasks.Queries.GetRepairTaskById;
using AutoFix.Application.Features.RepairTasks.Queries.GetRepairTasks;
using AutoFix.Contracts.Requests;
using AutoFix.Domain.RepairTasks.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace AutoFix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairTasksController : ControllerBase
    {

        private readonly IMediator _mediator;

        public RepairTasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRepairTaskCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsError)
            {
                return BadRequest(result.Errors);

            }
            return Ok(result.Value);

        }

    
        
        [HttpDelete("{repairTaskId:Guid}")]


     public async Task<IActionResult> Delete(Guid repairTaskId)
    {
            var repairTask = new RemoveRepairTaskCommand(repairTaskId);
        var result = await _mediator.Send(repairTask);
        if (result.IsError)
        {
            return BadRequest(result.Errors);

        }
        return Ok(result.Value);

    }


        [HttpPut("{repairTaskId:Guid}")]


        public async Task<IActionResult> Put(Guid repairTaskId, [FromBody] UpdateRepairTaskRequest request)
        {
            var parts = request.Parts.Select(p => new UpdateRepairTaskPartCommand(p.PartId, p.Name, p.Cost, p.Quantity)).ToList();



            var repairTask= new UpdateRepairTaskCommand(repairTaskId,request.Name,request.LaborCost,(RepairDurationInMinutes)request.EstimatedDurationInMins,parts);

            var result= await _mediator.Send(repairTask);
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);

        }

        [HttpGet("{repairTaskId:Guid}")]

        public async Task<IActionResult> GetById(Guid repairTaskId)
        {
            var repairTask= await _mediator.Send(new GetRepairTaskByIdQuery(repairTaskId));
            if (repairTask is null)
            {
                return NotFound();
            }

            return Ok(repairTask);
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var repairTasks = await _mediator.Send(new GetRepairTasksQuery());
            return Ok(repairTasks); 
        }
        
}
}
