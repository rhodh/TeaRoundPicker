using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.IntegrationTests.TestUtils;
using Xunit;

namespace WebAPI.IntegrationTests
{
    public class GetUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        public static IEnumerable<object[]> Users
        {
            get
            {
                return new[]
                {
                    new object[] { new { firstName = "Jane", lastName = "Doe", } },
                    new object[] { new { firstName = "Bob", lastName = "Smith"} }
                };
            }
        }

        [Theory]
        [MemberData(nameof(Users))]
        public async Task SutCanRetrieveCreatedUser(dynamic user)
        {
            var userId = (await _client.SendCreateUserRequest((object)user)).responseBody.Value<string>("id");

            var (responseBody, httpResponse) = await _client.SendGetUserRequest(userId);

            var id = responseBody.Value<string>("id");
            var firstName = responseBody.Value<string>("firstName");
            var lastName = responseBody.Value<string>("lastName");

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.Equal(userId, id);
            Assert.Equal(user.firstName, firstName);
            Assert.Equal(user.lastName, lastName);
        }
    }
}
