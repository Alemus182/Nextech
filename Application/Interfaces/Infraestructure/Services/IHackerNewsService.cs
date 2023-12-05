using Application.Dtos;

namespace Application.Interfaces.Infraestructure.Services
{
    public interface IHackerNewsService
    {
        public Task<IEnumerable<int>> GetNewestStories();
        public Task<IEnumerable<int>> GetStoriesByCategory(string Category);
        public Task<StoryItemDto> GetStoryById(int Id);
    }
}
