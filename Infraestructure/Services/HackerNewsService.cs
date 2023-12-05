using Application.Dtos;
using System.Text.Json;

namespace Application.Interfaces.Infraestructure.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private  IHttpClientFactory _httpClientFactory;
 
        public HackerNewsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<int>> GetNewestStories()
        {
            IEnumerable<int>? result = Enumerable.Empty<int>();
            HttpClient client = _httpClientFactory.CreateClient("hackernews");
            string url = $"/v0/newstories.json?print=pretty";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<IEnumerable<int>>(responseData);
            }

            return result;
        }

        public async Task<IEnumerable<int>> GetStoriesByCategory(string Category)
        {
            IEnumerable<int>? result = Enumerable.Empty<int>();
            HttpClient client = _httpClientFactory.CreateClient("hackernews");
            string url = $"/v0/{Category}.json?print=pretty";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<IEnumerable<int>>(responseData);
            }

            return result;
        }

        public async Task<StoryItemDto> GetStoryById(int Id)
        {
            StoryItemDto? result = null;
            HttpClient client = _httpClientFactory.CreateClient("hackernews");
            string url = $"/v0/item/{Id}.json?print=pretty";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<StoryItemDto>(responseData);
            }

            return result;
        }
    }
}
