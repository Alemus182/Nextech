using Api.Components;
using Application.Services.Auth.Commands;
using MediatR;

namespace Api.Routes
{
    public class AuthRouter : RouterBase
    {
        public AuthRouter(ILogger<AuthRouter> logger, IMediator mediatR)
        {
            Logger = logger;
            _mediatR= mediatR;
        }

        public override void AddRoutes(WebApplication app)
        {
            var group = app.MapGroup(ApiRoutes.Auth.Login).WithOpenApi();
            group.MapPost($"/", (SingInRequest SingIn) => Post(SingIn));
        }

        protected virtual async Task<IResult> Post(SingInRequest SingIn)
        {
            var result = await _mediatR.Send(SingIn);
            return Results.Ok(result);
        }
    }
}
