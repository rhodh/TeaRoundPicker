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
    public class CreateUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task SutReturnCreatedUserWithId()
        {
            var user = new
            {
                firstName = "Bob",
                lastName = "Smith",
            };

            var (responseBody, httpResponse) = await _client.SendCreateUserRequest(user);

            var id = new Guid(responseBody.Value<string>("id"));
            var firstName = responseBody.Value<string>("firstName");
            var lastName = responseBody.Value<string>("lastName");


            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal(user.firstName, firstName);
            Assert.Equal(user.lastName, lastName);
            Assert.Equal($"http://localhost/v1/Users/{id}", httpResponse.Headers.Location.ToString());
        }

        public static IEnumerable<object[]> InvalidUserBody
        {
            get
            {
                return new[]
                {
                    new object[] { new { lastName = "Smith"  } },
                    new object[] { new { FirstName = "", lastName = "Smith", } },
                    new object[] { new { FirstName = "Bob" } },
                    new object[] { new { FirstName = "Bob", lastName = ""} }
                };
            }
        }

        [Theory]
        [MemberData(nameof(InvalidUserBody))]
        public async Task SutInvalidBodyProducedBadRequest(object body)
        {
            var (_, httpResponse) = await _client.SendCreateUserRequest(body);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

    }
}
