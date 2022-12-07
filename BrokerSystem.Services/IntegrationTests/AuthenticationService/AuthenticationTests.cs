using AuthenticationService.Data.ViewModels.Auth;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.AuthenticationService
{
    public class AuthenticationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _testClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AuthenticationTests(WebApplicationFactory<Program> factory)
        {
            _testClient = factory.CreateClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        [Fact]
        public async Task AuthenticateUser_SuccessfullCase()
        {
            // Arrange
            var authenticationRequest =
                new AuthenticationRequest { Email = "broker@inbox.ru", Password = "12345678" };

            // Act
            var content = new StringContent(JsonSerializer.Serialize(authenticationRequest), Encoding.UTF8,
                "application/json");
            var response = await _testClient.PostAsync("api/Authentication/authenticate", content);

            //Assert
            response.EnsureSuccessStatusCode();
            response.Headers.Should().ContainKey(HeaderNames.SetCookie);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<AuthenticationResponse>(responseString, _jsonSerializerOptions);

            responseModel.Should().NotBeNull();
            responseModel.AccessToken.Should().NotBeNullOrEmpty();
            responseModel.Account.Email.Should().BeEquivalentTo(authenticationRequest.Email);
        }

        [Theory]
        [InlineData("broker@inbox.ru", "invalid_password")]
        [InlineData("invalid@inbox.ru", "12345678")]
        public async Task AuthenticateUser_FailedCase(string email, string password)
        {
            // Arrange
            var authenticationRequest =
                new AuthenticationRequest { Email = email, Password = password };

            // Act
            var content = new StringContent(JsonSerializer.Serialize(authenticationRequest), Encoding.UTF8,
                "application/json");
            var response = await _testClient.PostAsync("api/Authentication/authenticate", content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public void Dispose()
        {
            _testClient.Dispose();
        }
    }
}
