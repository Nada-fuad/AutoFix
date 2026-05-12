using AutoFix.Application.Features.Labors.Qieries.GetLabors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaborsController(ISender sender) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetLaborsQuery());

            if (result.IsError)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
