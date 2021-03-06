using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task SutBadRequestWhenBodyIsEmpty()
        {
            (Guid, Guid) users = await GetTwoUsers();

            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
            });

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }


        [Fact]
        public async Task SutBadRequestWhenParicipantssEmpty()
        {
            (Guid, Guid) users = await GetTwoUsers();

            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
                participants = new object[]
                {
                 
                }
            });

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
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
                participants = new object[]
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
        public async Task SutReturnBadRequestBothUserDoesNotExist()
        {
            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
                participants = new object[]
                {
                    new
                    {
                        userId = Guid.NewGuid()
                    },
                    new
                    {
                        userId = Guid.NewGuid()
                    }
                }
            });

            ProblemDetails details = responseBody.ToObject<ProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal("UsersNotFound", details.Type);
        }

        [Fact]
        public async Task SutReturnBadRequestOneUserDoesNotExist()
        {
            var userId = new Guid((await _client.SendCreateUserRequest(new
            {
                firstName = "Bob",
                lastName = "Smith",
            })).responseBody.Value<string>("id"));


            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
                participants = new object[]
                {
                    new
                    {
                        userId = Guid.NewGuid()
                    },
                    new
                    {
                        userId
                    }
                }
            });

            ProblemDetails details = responseBody.ToObject<ProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal("UsersNotFound", details.Type);
        }

        [Fact]
        public async Task SutReturnCreated()
        {
            (Guid, Guid) users = await GetTwoUsers();

            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
                participants = new object[]
                {
                    new
                    {
                        userId = users.Item1
                    },
                    new
                    {
                        userId = users.Item2
                    }
                }
            });

            var id = new Guid(responseBody.Value<string>("id"));

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal($"http://localhost/v1/DrinkRun/{id}", httpResponse.Headers.Location.ToString());
        }

        [Fact]
        public async Task SutReturnExpectedDrinkMaker()
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
            var (responseBody, _) = await _client.SendCreateDrinkRunRequest(new 
            {
                participants = new object[]
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

            var drinkMaker = responseBody.SelectToken("drinkMaker");

            Assert.Equal(expectedDrinkMakerId.ToString(), drinkMaker.Value<string>("id"));
            Assert.Equal(expectedDrinkMakerName.firstName, drinkMaker.Value<string>("firstName"));
            Assert.Equal(expectedDrinkMakerName.lastName, drinkMaker.Value<string>("lastName"));
        }

        [Fact]
        public async Task SutReturnUsersOrders()
        {
            (Guid, Guid) users = await GetTwoUsers();

            var (responseBody, httpResponse) = await _client.SendCreateDrinkRunRequest(new
            {
                participants = new object[]
                {
                    new
                    {
                        userId = users.Item1
                    },
                    new
                    {
                        userId = users.Item2
                    }
                }
            });

            var orders = responseBody.SelectToken("orders").ToObject<IEnumerable<DrinkOrdersV1>>();

            Assert.NotNull(orders);
            Assert.Contains(orders, o => o.UserId == users.Item1);
            Assert.Equal(3, orders.First(o => o.UserId == users.Item1).AdditionalSpecification.Count);
            Assert.Contains(orders, o => o.UserId == users.Item2);
            Assert.Equal(4, orders.First(o => o.UserId == users.Item2).AdditionalSpecification.Count);
        }

        private async Task<Guid> CreateUser(object user, object brew)
        {
            var userId = new Guid((await _client.SendCreateUserRequest(user)).responseBody.Value<string>("id"));
            await _client.SendCreateDrinkOrderRequest(userId, brew);
            return userId;
        }

        private async Task<(Guid, Guid)> GetTwoUsers()
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

            var otherUser = await CreateUser(new
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

            return (userId, otherUser);
        }


    }
}
