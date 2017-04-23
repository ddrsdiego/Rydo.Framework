using MediatR;
using Rydo.Framework.MediatR.Command;
using Rydo.Framework.MediatR.Response;
using System.Threading.Tasks;

namespace Rydo.Framework.MediatR.Handlres
{
    public abstract class HandlerAsync<TCommandAsync, TResponseAsync> : IAsyncRequestHandler<TCommandAsync, TResponseAsync>
        where TCommandAsync : CommandMessageAsync<TResponseAsync>
        where TResponseAsync : ResponseMessageAsync
    {

        private readonly IMediator _Mediator;
        protected IMediator Mediator
        {
            get { return _Mediator; }
        }

        public HandlerAsync(IMediator mediator)
        {
            _Mediator = mediator;
        }

        public abstract Task<TResponseAsync> Handle(TCommandAsync message);
    }
}
