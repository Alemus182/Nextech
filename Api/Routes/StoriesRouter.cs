using Api.Components;
using Application.Services.Stories.Querys;
using Application.Services.Stories.Responses;
using MediatR;

namespace Api.Routes
{
    public class StoriesRouter : RouterBase
    {
        public StoriesRouter(ILogger<StoriesRouter> logger, IMediator mediatR)
        {
            Logger = logger;
            _mediatR= mediatR;
        }

        /// <summary>
        /// Add routes
        /// </summary>
        /// <param name="app">A WebApplication object</param>
        public override void AddRoutes(WebApplication app)
        {
            var group = app.MapGroup(ApiRoutes.StoriesRoutes.Group).WithOpenApi().RequireAuthorization();
            
            group.MapGet(ApiRoutes.StoriesRoutes.GetNewestStories, (int page) => GetNewestStories(page))
                .Produces<StoriesResponse>()
                .Produces(400)
                .Produces(401)
                .Produces(500)
                ;
            group.MapPost(ApiRoutes.StoriesRoutes.FindStoriesByFilters, (FindStoriesByFilterRequest req) => FindStoriesByFilters(req))
                .Produces<StoriesResponse>()
                .Produces(400)
                .Produces(401)
                .Produces(500);
        }

        /// <summary>
        /// GET a colletion of newest stories.
        /// </summary>
        /// <returns>An IResult object with the collection</returns>
        protected virtual async  Task<IResult> GetNewestStories(int page)
        {
            Logger.LogInformation("Getting Newest Stories");
            GetNewstStoriesRequest req = new GetNewstStoriesRequest();
            req.Page = page;
            var result = await _mediatR.Send(req);
            return Results.Ok(result);
        }

        /// <summary>
        /// GET a colletion stories by filters like id and category
        /// </summary>
        /// <returns>An IResult object with the collection</returns>
        protected virtual async Task<IResult> FindStoriesByFilters(FindStoriesByFilterRequest req)
        {
            Logger.LogInformation("Find Stories by Filters");
            var result = await _mediatR.Send(req);
            return Results.Ok(result);
        }
    }
}
