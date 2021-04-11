using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebAPI.IntegrationTests
{
    public class GetWeatherForcast : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetWeatherForcast(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetForcastTestReturns200()
        {
            HttpResponseMessage result = await _client.GetAsync("/WeatherForecast");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
