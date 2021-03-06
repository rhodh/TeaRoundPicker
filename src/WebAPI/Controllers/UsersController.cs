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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto, ApiVersion version)
        {
            User user = await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id, version = version.ToString() }, user);
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUserById([FromRoute] Guid id)
        {
            User user = await _userService.GetUser(id);
            return Ok(user);
        }


        [HttpPost("{id}/drinkOrder")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DrinkOrder>> AddDrinkOrderToUser([FromRoute] Guid id, [FromBody] DrinkOrderDto drinkOrderDto, ApiVersion version)
        {
            DrinkOrder drinkOrder = await _userService.AddDrinkOrder(id, drinkOrderDto);
            return CreatedAtAction(nameof(GetDrinkOrderById), new { id = drinkOrder.Id, userId = id, version = version.ToString() }, drinkOrder);
        }

        [HttpGet("{userId}/DrinkOrder/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public Task<ActionResult<User>> GetDrinkOrderById([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
