using MediatR;
using Rydo.Framework.MediatR.Contratos;
using Rydo.Framework.MediatR.Response;

namespace Rydo.Framework.MediatR.Command
{
    public abstract class CommandMessage<TResponseMessage> : Message, IRequest<TResponseMessage>
        where TResponseMessage : ResponseMessage
    {
        public virtual TResponseMessage Response { get; }
    }
}
