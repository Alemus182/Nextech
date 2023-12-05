using Application.Dtos.Auth;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Test
{
    public class StoriesIntegrationTest
    {
        [Fact]
        public async Task GetNewestAuthFail()
        {
            // Arrange
            var app = new ApiApplication();

            // Act
            var client = app.CreateClient();

            var response = await client.GetAsync("/Api/Stories/Get-Newest/1");
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetNewestOk()
        {
            // Arrange
            var app = new ApiApplication();

            // Act
            var client = app.CreateClient();
            var body = new
            {
                username = "test@nexttech.com",
                password = "3454365464"
            };

            var dataToPost = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/login", dataToPost);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            SingInResponse responseAuth = JsonSerializer.Deserialize<SingInResponse>(responseBody);
            Assert.NotNull(responseAuth);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + responseAuth.token);
            response = await client.GetAsync("/Api/Stories/Get-Newest/0");

            responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetNewestValidation()
        {
            // Arrange
            var app = new ApiApplication();

            // Act
            var client = app.CreateClient();
            var body = new
            {
                username = "test@nexttech.com",
                password = "3454365464"
            };

            var dataToPost = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/login", dataToPost);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            SingInResponse responseAuth = JsonSerializer.Deserialize<SingInResponse>(responseBody);
            Assert.NotNull(responseAuth);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + responseAuth.token);
            response = await client.GetAsync("/Api/Stories/Get-Newest/-1");

            responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetStoriesbyFilterCategoryOk()
        {
            // Arrange
            var app = new ApiApplication();

            // Act
            var client = app.CreateClient();
            var body = new
            {
                username = "test@nexttech.com",
                password = "3454365464"
            };

            var dataToPost = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/login", dataToPost);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            SingInResponse responseAuth = JsonSerializer.Deserialize<SingInResponse>(responseBody);
            Assert.NotNull(responseAuth);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + responseAuth.token);

            var bodyToPost = new
            {
                isCategory = true,
                category = "topstories",
                page = 0
            };

            var dataToPostFilter = new StringContent(JsonSerializer.Serialize(bodyToPost), Encoding.UTF8, "application/json");

            response = await client.PostAsync("/Api/Stories/Find-Stories-by-Filter/",dataToPostFilter);

            responseBody = await response.Content.ReadAsStringAsync();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStoriesbyFilterIdOk()
        {
            // Arrange
            var app = new ApiApplication();

            // Act
            var client = app.CreateClient();
            var body = new
            {
                username = "test@nexttech.com",
                password = "3454365464"
            };

            var dataToPost = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/login", dataToPost);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            SingInResponse responseAuth = JsonSerializer.Deserialize<SingInResponse>(responseBody);
            Assert.NotNull(responseAuth);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + responseAuth.token);

            var bodyToPost = new
            {
                isCategory = false,
                category = "",
                id = 126993,
                page = 0
            };

            var dataToPostFilter = new StringContent(JsonSerializer.Serialize(bodyToPost), Encoding.UTF8, "application/json");

            response = await client.PostAsync("/Api/Stories/Find-Stories-by-Filter/", dataToPostFilter);

            responseBody = await response.Content.ReadAsStringAsync();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStoriesbyFilterValidate()
        {
            // Arrange
            var app = new ApiApplication();

            // Act
            var client = app.CreateClient();
            var body = new
            {
                username = "test@nexttech.com",
                password = "3454365464"
            };

            var dataToPost = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/login", dataToPost);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            SingInResponse responseAuth = JsonSerializer.Deserialize<SingInResponse>(responseBody);
            Assert.NotNull(responseAuth);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + responseAuth.token);

            var bodyToPost = new
            {
                isCategory = true,
                category = "othercategory",
                page = 0
            };

            var dataToPostFilter = new StringContent(JsonSerializer.Serialize(bodyToPost), Encoding.UTF8, "application/json");

            response = await client.PostAsync("/Api/Stories/Find-Stories-by-Filter/", dataToPostFilter);

            responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
