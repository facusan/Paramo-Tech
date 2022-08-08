using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserControllerTester", DisableParallelization = true)]
    public class UserControllerTester : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly HttpClient _client;
        public UserControllerTester(WebApplicationFactory<Api.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void CreateUserOkTest()
        {
            var newUser = new User
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var result = await CreateUserAsync(_client, newUser);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, (int)result.StatusCode);
        }

        [Fact]
        public async void CreateDuplicatedUserErrorTest()
        {
            var newUser = new User
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            var result = await CreateUserAsync(_client, newUser);
            var resultMessage = await result.Content.ReadAsStringAsync();

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status409Conflict,(int) result.StatusCode);
            Assert.Equal("The user is duplicated", resultMessage);
        }

        [Fact]
        public async void CreateUserWithoutRequiredDataErrorTest()
        {
            var newUser = new User();
            var result = await CreateUserAsync(_client, newUser);
            var resultMessage = await result.Content.ReadAsStringAsync();

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, (int)result.StatusCode);
        }

        private static async Task<HttpResponseMessage> CreateUserAsync(HttpClient client, User newUser)
        {
            return await client.PostAsync("/create-user",
                new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                });
        }
    }
}
