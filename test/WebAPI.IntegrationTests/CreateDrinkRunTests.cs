using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.IntegrationTests.ExpectedModels;
using WebAPI.IntegrationTests.TestUtils;
using Xunit;

namespace WebAPI.IntegrationTests
{
    public class CreateDrinkRunTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateDrinkRunTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SutReturnBadRequestIfAUserDoesNotHaveAnyOrders()
        {
            var userId = new Guid((await _client.SendCreateUserRequest(new
            {
                firstName = "Bob",
                lastName = "Smith",
            })).responseBody.Value<string>("id"));

            var otherUserId = await CreateUser(new
            {
                firstName = "John",
                lastName = "William",
            },
            new
            {
                type = "InstantCoffee",
                name = "Morning Joe",
                additionalSpecification = new Dictionary<string, string>
                {
                    { "brand", "nescafe" },
                    { "amount", "2 tea spoons"},
                    { "sugar", "1 table spoon" },
                    { "milk", "" }
                }
            });

            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
                particpants = new object[]
                {
                    new
                    {
                        userId
                    },
                    new
                    {
                        userId = otherUserId
                    }
                }
            });

            ProblemDetails details = responseBody.ToObject<ProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal("OrderNotDefined", details.Type);
        }

        [Fact]
        public async Task SutReturnCreatedUserWithId()
        {
            var userId = await CreateUser(new
            {
                firstName = "Bob",
                lastName = "Smith",
            }, 
            new
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

            var expectedDrinkMakerName = new
            {
                firstName = "John",
                lastName = "William",
            };
            var expectedDrinkMakerId = await CreateUser(expectedDrinkMakerName, 
            new
            {
                type = "InstantCoffee",
                name = "Morning Joe",
                additionalSpecification = new Dictionary<string, string>
                {
                    { "brand", "nescafe" },
                    { "amount", "2 tea spoons"},
                    { "sugar", "1 table spoon" },
                    { "milk", "" }
                }
            });

            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new 
            {
                particpants = new object[]
                {
                    new
                    {
                        userId
                    },
                    new
                    {
                        userId = expectedDrinkMakerId
                    }
                } 
            });

            var id = new Guid(responseBody.Value<string>("id"));
            var drinkMaker = responseBody.SelectToken("drinkMaker");
            var orders = responseBody.SelectToken("orders").ToObject<IEnumerable<DrinkOrdersV1>>();

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal(expectedDrinkMakerId.ToString(), drinkMaker.Value<string>("id"));
            Assert.Equal(expectedDrinkMakerName.firstName, drinkMaker.Value<string>("firstName"));
            Assert.Equal(expectedDrinkMakerName.lastName, drinkMaker.Value<string>("lastName"));
            
            Assert.NotNull(orders);
            Assert.Contains(orders, o => o.UserId == userId);
            Assert.Contains(orders, o => o.UserId == expectedDrinkMakerId);

            Assert.Equal($"http://localhost/v1/DrinkRun/{id}", httpResponse.Headers.Location.ToString());
        }
       
        private async Task<Guid> CreateUser(object user, object brew)
        {
            var userId = new Guid((await _client.SendCreateUserRequest(user)).responseBody.Value<string>("id"));
            await _client.SendCreateDrinkOrderRequest(userId, brew);
            return userId;
        }

    }
}
