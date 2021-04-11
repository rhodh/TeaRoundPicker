using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebAPI.IntegrationTests.TestUtils
{
    public static class HttpClientExtensions
    {
        public static async Task<(JObject responseBody, HttpResponseMessage httpResponse)> SendCreateUserRequest(
            this HttpClient client, object body)
        {
            var content = new StringContent(JObject.FromObject(body).ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("v1/users", content);
            var json = await response.Content.ReadAsStringAsync();

            try
            {
                JObject responseBody = !string.IsNullOrEmpty(json) ? JObject.Parse(json) : null;
                return (responseBody, response);
            }
            catch (Exception)
            {
                Assert.True(false, json);
                throw;
            }
        }

        public static async Task<(JObject responseBody, HttpResponseMessage httpResponse)> SendGetUserRequest(
           this HttpClient client, string id)
        {
            HttpResponseMessage response = await client.GetAsync($"v1/users/{id}");
            var json = await response.Content.ReadAsStringAsync();

            try
            {
                JObject responseBody = !string.IsNullOrEmpty(json) ? JObject.Parse(json) : null;
                return (responseBody, response);
            }
            catch (Exception)
            {
                Assert.True(false, json);
                throw;
            }
        }
    }
}
