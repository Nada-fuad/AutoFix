using AutoFix.Application.Features.Labors.Dtos;
using AutoFix.Application.Features.Labors.Qieries.GetLabors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace AutoFix.Api.Controllers
{
    [Route("api/labors")]
    public class LaborsController(ISender sender) : ApiController
    {
        [HttpGet]

        [ProducesResponseType(typeof(List<LaborDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves the list of available labor definitions.")]
        [EndpointDescription("Returns all labor records associated with the system, accessible only to users with the Manager role.")]
        [EndpointName("GetLabors")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetLaborsQuery());

            return result.Match(
            response => Ok(response),
            Problem);
        }
    }
}
