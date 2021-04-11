using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.IntegrationTests.TestUtils;
using Xunit;
using Xunit.Extensions;

namespace WebAPI.IntegrationTests
{
    public class CreateDrinkOrderTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateDrinkOrderTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task SutReturnCreatedUserWithId()
        {
            var userId = new Guid((await _client.SendCreateUserRequest(new
            {
                firstName = "Bob",
                lastName = "Smith",
            })).responseBody.Value<string>("id"));

            var (responseBody, httpResponse) = await _client.SendCreateDrinkOrderRequest(userId, new
            {
                type = "Tea",
                name = "Fave Brew",
                additionalSpecification = new Dictionary<string, string>
                {
                    { "milk", "lots" },
                    { "sugar", "2 tea spoons"},
                    { "brewTime", "2 min" }
                }
            });

            var id = new Guid(responseBody.Value<string>("id"));

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal($"http://localhost/v1/Users/{userId}/DrinkOrder/{id}", httpResponse.Headers.Location.ToString());
        }

    }
}
