using Application.Common.Helpers;
using Application.Dtos;
using Application.Interfaces.Infraestructure.Services;
using Application.Services.Stories.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services.Stories.Querys
{
    public class FindStoriesByFilterRequest : IRequest<StoriesResponse>
    {
        public bool IsCategory { get; set; }
        public string Category { get; set; } = string.Empty;
        public int Id { get; set; } = 0;
        public int Page { get; set; } = 0;
        public class FindStoriesByFilterHandler : IRequestHandler<FindStoriesByFilterRequest, StoriesResponse>
        {
            private readonly IHackerNewsService _HackerNewsService;

            private readonly IMemoryCache _memoryCache;

            private const int PageSize = 20;

            public FindStoriesByFilterHandler(IHackerNewsService HackerNewsService, IMemoryCache memoryCache)
            {
                _HackerNewsService = HackerNewsService;
                _memoryCache = memoryCache;
            }

            public async Task<StoriesResponse> Handle(FindStoriesByFilterRequest request, CancellationToken cancellationToken)
            {
                var result = new StoriesResponse();
                IList<StoryItemDto>? listConsolidated;

                if (request.IsCategory)
                {
                    listConsolidated = _memoryCache.Get<IList<StoryItemDto>>(request.Category);

                    if (listConsolidated is null)
                    {
                        var idStories = await _HackerNewsService.GetStoriesByCategory(request.Category);

                        listConsolidated = await idStories.SelectAsync(async idStory =>
                        {
                            var storyItem = await _HackerNewsService.GetStoryById(idStory);
                            return storyItem;
                        });

                        _memoryCache.Set(request.Category, listConsolidated, new TimeSpan(1, 0, 0));
                    }

                    result.totalRecords = listConsolidated.Count;
                    result.totalPages = listConsolidated.Count / PageSize;
                    result.currentPage = request.Page;
                    result.pageSize = PageSize;
                    result.data = listConsolidated.
                                     Skip(request.Page * PageSize).
                                     Take(PageSize).ToList();
                }
                else
                {
                    var storyItem = await _HackerNewsService.GetStoryById(request.Id);
                    if (storyItem is not null)
                    {
                        listConsolidated = new List<StoryItemDto> { storyItem };
                        result.totalRecords = result.totalPages = result.currentPage = 1;
                        result.pageSize = PageSize;
                        result.data = listConsolidated;
                    }
                }

                return result;
            }
        }
    }
}
