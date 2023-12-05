using Application.Interfaces.Infraestructure.Services;
using Application.Services.Stories.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Application.Dtos;
using Application.Common.Helpers;

namespace Application.Services.Stories.Querys
{
    public class GetNewstStoriesRequest : IRequest<StoriesResponse>
    {
        public int Page { get; set; }
        public class GetNewstStoriesHandler : IRequestHandler<GetNewstStoriesRequest, StoriesResponse>
        {
            private readonly IHackerNewsService _HackerNewsService;

            private readonly IMemoryCache _memoryCache;

            private const int PageSize = 200;

            public GetNewstStoriesHandler(IHackerNewsService HackerNewsService, IMemoryCache memoryCache)
            {
                _HackerNewsService = HackerNewsService;
                _memoryCache = memoryCache;
            }

            public async Task<StoriesResponse> Handle(GetNewstStoriesRequest request, CancellationToken cancellationToken)
            {
                var result = new StoriesResponse();
                var idStories = await _HackerNewsService.GetNewestStories();
                IList<StoryItemDto> ? listConsolidated;

                listConsolidated = _memoryCache.Get<IList<StoryItemDto>>("newstories");

                if (listConsolidated is null)
                {
                    listConsolidated = await idStories.SelectAsync(async idStory =>
                     {
                         var storyItem = await _HackerNewsService.GetStoryById(idStory);
                         return storyItem;
                     });

                    _memoryCache.Set("newstories", listConsolidated, new TimeSpan(1, 0, 0));
                }

                result.totalRecords = listConsolidated.Count;
                result.totalPages = listConsolidated.Count / PageSize;
                result.currentPage = request.Page;
                result.pageSize = PageSize;
                result.data = listConsolidated.
                                 Skip(request.Page * PageSize).
                                 Take(PageSize).ToList();
                return result;
            }
        }
    }
}
