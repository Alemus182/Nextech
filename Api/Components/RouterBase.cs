using MediatR;

namespace Api.Components
{
    public class RouterBase
    {
        protected ILogger Logger;

        protected IMediator _mediatR;
        public virtual void AddRoutes(WebApplication app)
        {

        }
    }
}
