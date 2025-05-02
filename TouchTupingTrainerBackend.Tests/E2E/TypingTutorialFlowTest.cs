using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Models;

namespace TouchTupingTrainerBackend.Tests.E2E;

public class TypingTutorialFlowTest : IClassFixture<WebApplicationFactory<Program>>
{
    readonly HttpClient _client;

    public TypingTutorialFlowTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RegisterAndLogin_ShouldWorkAsExpected()
    {
        // register new user
        var registerRequest = "/register";

        var registerPayload = new
        {
            Email = $"{Guid.NewGuid().ToString()}-user1@example.com",
            Password = "Password1!"
        };

        var registerResponse = await _client.PostAsJsonAsync(registerRequest, registerPayload);
        Assert.True(registerResponse.IsSuccessStatusCode);

        // login user
        var loginRequest = "/login";

        var loginPayload = new
        {
            Email = registerPayload.Email,
            Password = registerPayload.Password
        };

        var loginResponse = await _client.PostAsJsonAsync(loginRequest, loginPayload);
        Assert.True(loginResponse.IsSuccessStatusCode);
    }

    [Fact]
    public async Task PassRandomTestSet_WithUnautorizedUser_ShouldWorkAsExpected()
    {
        // random test set receiving
        var randomTestRequest = "api/test/get-random-test-set?layoutid=1";

        var testResponse = await _client.GetAsync(randomTestRequest);
        Assert.True(testResponse.IsSuccessStatusCode);

        string testResponseBody = await testResponse.Content.ReadAsStringAsync();
        TestingMaterial material = JsonSerializer.Deserialize<TestingMaterial>(testResponseBody, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // passing test
        var passTestRequest = "api/test/complete";

        var expectedIdKey = "id";
        var expectedAccuracyKey = "accuracy";
        var expectedSpeedKey = "speed";
        var expectedCreatedAtKey = "createdAt";

        var requestBody = new TestCompleteRequest
        {
            TestingMaterial = new TestingMaterial
            {
                Id = material.Id,
                Text = material.Text
            },
            MistakesCount = 1,
            Duration = 150
        };

        var passTestResponce = await _client.PostAsJsonAsync(passTestRequest, requestBody);
        Assert.True(passTestResponce.IsSuccessStatusCode);

        string resultResponseBody = await passTestResponce.Content.ReadAsStringAsync();
        Assert.Contains(expectedIdKey, resultResponseBody);
        Assert.Contains(expectedAccuracyKey, resultResponseBody);
        Assert.Contains(expectedSpeedKey, resultResponseBody);
        Assert.Contains(expectedCreatedAtKey, resultResponseBody);
    }

    [Fact]
    public async Task PassRandomTestSet_WithAuthorizedUser_ShouldWorkAsExpected()
    {
        // register new user
        var registerRequest = "/register";

        var registerPayload = new
        {
            Email = $"{Guid.NewGuid().ToString()}-user1@example.com",
            Password = "Password1!"
        };

        var testResponse = await _client.PostAsJsonAsync(registerRequest, registerPayload);
        Assert.True(testResponse.IsSuccessStatusCode);

        // login user
        var loginRequest = "/login";

        var loginPayload = new
        {
            Email = registerPayload.Email,
            Password = registerPayload.Password
        };

        var loginResponse = await _client.PostAsJsonAsync(loginRequest, loginPayload);
        Assert.True(loginResponse.IsSuccessStatusCode);

        // set user creditals
        var tokenKey = "accessToken";
        var bearerKey = "Bearer";

        var loginData = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
        var token = loginData.GetProperty(tokenKey).ToString();
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(bearerKey, tokenKey);

        // random test set receiving
        var randomTestRequest = "api/test/get-random-test-set?layoutid=1";

        var registerResponse = await _client.GetAsync(randomTestRequest);
        Assert.True(registerResponse.IsSuccessStatusCode);

        string testResponseBody = await registerResponse.Content.ReadAsStringAsync();
        TestingMaterial material = JsonSerializer.Deserialize<TestingMaterial>(testResponseBody, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // passing test
        var passTestRequest = "api/test/complete";

        var expectedIdKey = "id";
        var expectedAccuracyKey = "accuracy";
        var expectedSpeedKey = "speed";
        var expectedCreatedAtKey = "createdAt";

        var requestBody = new TestCompleteRequest
        {
            TestingMaterial = new TestingMaterial
            {
                Id = material.Id,
                Text = material.Text
            },
            MistakesCount = 1,
            Duration = 150
        };

        var passTestResponce = await _client.PostAsJsonAsync(passTestRequest, requestBody);
        Assert.True(passTestResponce.IsSuccessStatusCode);

        string resultResponseBody = await passTestResponce.Content.ReadAsStringAsync();
        Assert.Contains(expectedIdKey, resultResponseBody);
        Assert.Contains(expectedAccuracyKey, resultResponseBody);
        Assert.Contains(expectedSpeedKey, resultResponseBody);
        Assert.Contains(expectedCreatedAtKey, resultResponseBody);
    }
}
