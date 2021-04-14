using Application.Services;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DrinkRunController : ControllerBase
    {
        private readonly ILogger<DrinkRunController> _logger;
        private readonly IDrinkRunService _drinkRunService;

        public DrinkRunController(ILogger<DrinkRunController> logger, IDrinkRunService drinkRunService)
        {
            _drinkRunService = drinkRunService ?? throw new ArgumentNullException(nameof(drinkRunService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DrinkRun), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDrinkRun([FromBody] DrinkRunDto drinkRunDto, ApiVersion version)
        {
            DrinkRun drinkRun = await _drinkRunService.CreateDrinkRun(drinkRunDto);
            return CreatedAtAction(nameof(GetDrinkRunById), new { id = drinkRun.Id, version = version.ToString() }, drinkRun);
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public Task<ActionResult<User>> GetDrinkRunById([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
