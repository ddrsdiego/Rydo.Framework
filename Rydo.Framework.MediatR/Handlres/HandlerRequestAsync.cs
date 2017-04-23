using MediatR;
using Rydo.Framework.MediatR.Command;
using Rydo.Framework.MediatR.Response;

namespace Rydo.Framework.MediatR.Handlres
{
    public abstract class HandlerRequestAsync<TCommandAsync, TResponseAsync> : HandlerAsync<TCommandAsync, TResponseAsync>
        where TCommandAsync : CommandMessageAsync<TResponseAsync>
        where TResponseAsync : ResponseMessageAsync
    {
        public HandlerRequestAsync(IMediator mediator)
            : base(mediator)
        {

        }
    }
}
