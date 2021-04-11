using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.UnitTests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void SutImplementsController()
        {
            var sut = new WeatherForecastController(Mock.Of<ILogger<WeatherForecastController>>());
            Assert.IsAssignableFrom<ControllerBase>(sut);
        }
    }
}
