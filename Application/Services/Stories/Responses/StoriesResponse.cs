using Application.Dtos;

namespace Application.Services.Stories.Responses
{
    public record StoriesResponse 
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int totalPages { get; set; }
        public IList<StoryItemDto> data { get;set; }
    }
}
