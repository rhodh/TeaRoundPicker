using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto, ApiVersion version)
        {
            User user = new() { Id = Guid.NewGuid(), FirstName = userDto.FirstName, LastName = userDto.LastName };
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id, version = version.ToString() }, user);
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUserById([FromRoute] Guid userId)
        {
            return Ok(new User { Id = userId });
        }
    }
}
