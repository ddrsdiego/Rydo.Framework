using MediatR;
using Rydo.Framework.MediatR.Command;
using Rydo.Framework.MediatR.Response;

namespace Rydo.Framework.MediatR.Handlres
{
    public abstract class HandlerRequest<TCommandMessage, TResponseMessage> : Handler, IRequestHandler<TCommandMessage, TResponseMessage>
        where TCommandMessage : CommandMessage<TResponseMessage>
        where TResponseMessage : ResponseMessage
    {
        public HandlerRequest(IMediator mediator)
            : base(mediator)
        {
        }

        public abstract TResponseMessage Handle(TCommandMessage message);
    }
}
